using DSharpPlus;
using DSharpPlus.Commands;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Presentation.Discord.Configuration;
using Presentation.Discord.Logging;
using DSharpPlus.EventArgs;
using Presentation.Discord.Exceptions;

namespace Presentation.Discord;
public class DiscordBotApplication
{
    private readonly DiscordApplicationConfiguration _configuration;
    private readonly ILogger<DiscordBotApplication> _logger;
#pragma warning disable IDE0052 // Remove unread private members
    private readonly IServiceProvider _services;
#pragma warning restore IDE0052 // Remove unread private members

    public DiscordClient DiscordClient { get; init; }
    public CommandsExtension CommandsExtension { get; init; }

    public DiscordBotApplication(IServiceProvider services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _configuration = services.GetRequiredService<IOptions<DiscordApplicationConfiguration>>().Value;
        _logger = services.GetRequiredService<ILogger<DiscordBotApplication>>();

        _logger.LogDiscordStartupMessage("Configuring Client");

        DiscordClient = new DiscordClient(
            new DiscordConfiguration()
            {
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
                Token = _configuration.Token,
                TokenType = TokenType.Bot,
                LoggerFactory = services.GetRequiredService<ILoggerFactory>(),
            });

        _logger.LogDiscordStartupMessage("Registering Commands");
        CommandsExtension = DiscordClient.UseCommands(new CommandsConfiguration
        {
            DebugGuildId = _configuration.DebugGuildId,
            ServiceProvider = services,
            RegisterDefaultCommandProcessors = true,
            UseDefaultCommandErrorHandler = true,
        });

        DiscordClient.GuildDownloadCompleted += DiscordClient_GuildDownloadCompleted;
    }

    private async Task DiscordClient_GuildDownloadCompleted(DiscordClient sender, GuildDownloadCompletedEventArgs args)
    {
        var guild = args.Guilds.Select(g => g.Value)
            .FirstOrDefault() ?? throw DiscordConfigurationException.NoGuilds();

        var channel = guild.Channels.Select(c => c.Value)
            .FirstOrDefault(c => !c.IsCategory) ?? throw DiscordConfigurationException.NoChannels();

        _logger.LogDebug("Channels:{@channelCount}", guild.Channels.Count);

        await channel.SendMessageAsync("Hello world");
    }

    public Task Connect()
    {
        _logger.LogInformation("Connecting to Gateway");
        return DiscordClient.ConnectAsync();
    }

    public Task Disconnect(string reason, string disconnectedBy = "Host")
    {
        _logger.LogDisconnect(reason, disconnectedBy);

        return DiscordClient.DisconnectAsync();
    }
}
