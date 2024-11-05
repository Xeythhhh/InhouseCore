using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;

using SharedKernel.Primitives.Result;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Domain.Champions;
using System.Text.Json;
using SharedKernel.Primitives.Reasons;
using SharedKernel;

[assembly: InternalsVisibleTo("Infrastructure.UnitTests")]
namespace Infrastructure;

/// <summary>Class to reference the Infrastructure <see cref="Assembly"/>.</summary>
public static class InfrastructureAssembly
{
    /// <summary>A Reference to the Infrastructure <see cref="Assembly"/>.</summary>
    public static Assembly Reference => typeof(InfrastructureAssembly).Assembly;

    /// <summary>Registers Entity Framework services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    /// <exception cref="ConfigurationErrorsException"></exception>
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation.</returns>
    private static IHostApplicationBuilder AddEntityFrameworkServices(this IHostApplicationBuilder builder)
    {
        Result registerConvertersResult = Id.RegisterConverters();
        if (registerConvertersResult.IsFailed) throw new ConfigurationErrorsException(registerConvertersResult.Errors[0].Message); //todo

        Id.RegisterGeneratorId(builder.Configuration.GetValue<int>(AppConstants.Configuration.EfCoreIdGenId));

        return builder;
    }

    /// <summary>Registers Infrastructure services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation.</returns>
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddEntityFrameworkServices();

        builder.Services.Scan(selector =>
            selector.FromAssemblies(Reference)
                .AddClasses()
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        return builder;
    }

    /// <summary>Configures database usage for the application.</summary>
    /// <param name="app">The host application.</param>
    public static void UseDatabase(this IHost app)
    {
        app.Services.EnsureDatabaseMigrated();
        //app.Services.UseDatabaseSeed();
    }

    private record AugmentSeedData(string Name, string Target, string Color);
    private record ChampionSeedData(string Name, string Role, AugmentSeedData[] Augments);
    /// <summary>Seeds the database with initial data.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The <see cref="IServiceProvider"/> for chained invocation.</returns>
    private static IServiceProvider UseDatabaseSeed(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ILogger<IServiceProvider> logger = scope.ServiceProvider.GetRequiredService<ILogger<IServiceProvider>>();

        int championCount = 0;
        int augmentTotal = 0;

        if (!dbContext.Champions.Any()) // Check if data exists to avoid duplicating entries
        {
            string jsonFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "dbSeed.json");
            string jsonData = File.ReadAllText(jsonFilePath);
            List<ChampionSeedData>? seedData = JsonSerializer.Deserialize<List<ChampionSeedData>>(jsonData);

            foreach (ChampionSeedData championData in seedData ?? new List<ChampionSeedData>())
            {
                int augmentCount = 0;

                Result<Champion> championResult = Champion.Create(championData.Name, championData.Role)
                    .Ensure(champion => !dbContext.Champions.Any(c => c.Name == champion.Name),
                        new Error("Duplicate champion name in seed data"))
                    .Bind(dbContext.Champions.Add)
                    .Map(entity => entity.Entity);

                if (championResult.IsFailed) throw new Exception($"Failed to parse champion seed data for '{championData.Name}'");

                foreach (AugmentSeedData augmentData in championData.Augments)
                {
                    Result<Champion> augmentResult = championResult.Value.AddAugment(augmentData.Name, augmentData.Target, augmentData.Color);
                    if (augmentResult.IsFailed) throw new Exception($"Failed to parse champion augment seed data for '{championData.Name}/{augmentData.Name}'");
                    augmentCount++;
                }

                augmentTotal += augmentCount;
                championCount++;

                logger.LogInformation("Adding [{ChampionCount}] '{ChampionName}' with {AugmentCount} augments.",
                    championCount, championData.Name, augmentCount);
            }

            dbContext.SaveChanges();
        }

        logger.LogInformation("Added {ChampionCount} champions and {AugmentTotal} augments.",
            championCount, augmentTotal);

        return serviceProvider;
    }

    /// <summary>Ensures that the database migrations are applied.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    private static void EnsureDatabaseMigrated(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!dbContext.Database.GetPendingMigrations().Any()) return;

        scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>()
            .LogInformation("Migrating Database");

        dbContext.Database.Migrate();
        dbContext.SaveChanges();
    }
}
