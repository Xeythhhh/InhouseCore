using System.Text.Json;

using Domain.Champions;
using Domain.ReferenceData;

using Infrastructure;

using Microsoft.EntityFrameworkCore;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Api.Services;

public class FolderWatcherService(ILogger<FolderWatcherService> logger, IServiceProvider serviceProvider)
    : BackgroundService
{
    private readonly string _baseFolder = AppContext.BaseDirectory;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await EnsureDatabaseInitializedAsync(cancellationToken);
        WatchGameFolders(cancellationToken);
    }

    private async Task EnsureDatabaseInitializedAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                // Attempt to connect to the database
                if (await dbContext.Database.CanConnectAsync(cancellationToken))
                {
                    logger.LogInformation("Database is ready. Starting FolderWatcherService.");
                    break;
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Database not ready. Retrying...");
            }

            // Wait before retrying
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }

    private void WatchGameFolders(CancellationToken cancellationToken)
    {
        using FileSystemWatcher watcher = new(_baseFolder)
        {
            NotifyFilter = NotifyFilters.DirectoryName,
            Filter = "Game-*",
            IncludeSubdirectories = false,
            EnableRaisingEvents = true
        };

        watcher.Created += async (sender, e) =>
        {
            if (!Directory.Exists(e.FullPath)) return;

            logger.LogInformation("New folder detected: {Path}", e.FullPath);
            await ParseGameDataAsync(e.FullPath, cancellationToken);
        };

        while (!cancellationToken.IsCancellationRequested)
        {
            Thread.Sleep(1000);
        }
    }

    private record AugmentData(string Name, string Target, string Color);
    private record ChampionData(string Name, string Role, AugmentData[] Augments);

    internal async Task ParseGameDataAsync(string folderPath, CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Game game = await ParseGameReferenceData(folderPath, dbContext, cancellationToken);

        InitializeDomainObjects(game);

        string championsPath = Path.Combine(folderPath, "Champions");
        if (Directory.Exists(championsPath))
        {
            foreach (string championDir in Directory.GetDirectories(championsPath, "*", SearchOption.TopDirectoryOnly))
            {
                ChampionData championData = await ParseChampionData(dbContext, game, championDir, cancellationToken);
                logger.LogInformation("'{Champion}' champion data parsed and loaded from {ChampionDir}", championData.Name, championDir);
            }
        }
        else
        {
            logger.LogWarning("No 'Champions' directory found in {FolderPath}", folderPath);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("'{Game}' game data parsed and loaded from {FolderPath}", game.Name, folderPath);
    }

    private async Task<ChampionData> ParseChampionData(ApplicationDbContext dbContext, Game game, string championDir, CancellationToken cancellationToken)
    {
        string championDataPath = Path.Combine(championDir, "data.json");
        if (!File.Exists(championDataPath)) throw new FileNotFoundException(championDataPath);

        string championJsonData = await File.ReadAllTextAsync(championDataPath, cancellationToken);
        ChampionData? championData = JsonSerializer.Deserialize<ChampionData>(championJsonData);
        ArgumentNullException.ThrowIfNull(championData);

        if (dbContext.Champions.Any(c => c.Name == championData.Name)) return championData;

        // todo replace localhost:7420

        Result<Champion> championResult = Champion.Create(
                championData.Name,
                championData.Role,
                $"https://localhost:7420/api/Assets/Game-{game.Name}/Champions/{championData.Name}/portrait.png",
                $"https://localhost:7420/api/Assets/Game-{game.Name}/Champions/{championData.Name}/wide.png")
            .Ensure(champion => !dbContext.Champions.Any(c => c.Name == champion.Name),
                new Error("Duplicate champion name in seed data"))
            .Tap(champion => champion.SetGame(game))
            .Bind(dbContext.Champions.Add)
            .Map(entity => entity.Entity);

        if (championResult.IsFailed) throw new Exception($"Failed to parse champion data for '{championData.Name}'");

        foreach (AugmentData augmentData in championData.Augments)
        {
            if (!game.AugmentColors.Select(c => c.Split("|")[1]).Contains(augmentData.Color))
                throw new InvalidOperationException($"Augment color '{augmentData.Color}' not supported. (supported range: [{string.Join(", ", game.AugmentColors)}])");

            string? augmentIconPath = Directory
                .EnumerateFiles(championDir, $"{augmentData.Target.ToLower()}_*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();

            if (augmentIconPath == null)
            {
                logger.LogWarning("No file found for augment target '{Target}' in directory '{ChampionDir}'", augmentData.Target, championDir);
                augmentIconPath = $"https://localhost:7420/api/Assets/Game-{game.Name}/Unknown _square.png";
            }
            else
            {
                string augmentIconFileName = Path.GetFileName(augmentIconPath);
                augmentIconPath = $"https://localhost:7420/api/Assets/Game-{game.Name}/Champions/{championData.Name}/{augmentIconFileName}";
            }

            ParseAugmentData(championResult.Value, augmentData, augmentIconPath);
        }

        return championData;
    }

    private static void ParseAugmentData(Champion champion, AugmentData augmentData, string icon)
    {
        Result<Champion> augmentResult = champion.AddAugment(
            augmentData.Name,
            augmentData.Target,
            augmentData.Color,
            icon);
        if (augmentResult.IsFailed) throw new Exception($"Failed to parse champion augment data for '{champion.Name}/{augmentData.Name}'");
    }

    private static void InitializeDomainObjects(Game game)
    {
        Champion.ChampionRole.AddValidValues(
            game.Name,
            game.Roles.Select(r => $"{game.Name}-{r}").ToArray());

        Champion.Augment.AugmentTarget.AddValidValues(
            game.Name,
            game.Augments.Select(a => $"{game.Name}-{a}").ToArray());
    }

    private static async Task<Game> ParseGameReferenceData(string folderPath, ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        string gameDataPath = Path.Combine(folderPath, "data.json");
        if (!File.Exists(gameDataPath)) throw new FileNotFoundException(gameDataPath);

        string gameDataJson = await File.ReadAllTextAsync(gameDataPath, cancellationToken);
        Game? game = JsonSerializer.Deserialize<Game>(gameDataJson);
        ArgumentNullException.ThrowIfNull(game);

        if (!await dbContext.Games.AnyAsync(g => g.Name == game.Name, cancellationToken))
        {
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return game;
    }
}