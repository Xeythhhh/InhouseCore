using Microsoft.Extensions.Logging;

namespace Presentation.Discord.Logging;
public static partial class LoggingExtensions
{
    [LoggerMessage(LogLevel.Information,
"\x1b[45m[Discord Startup]{@message}")]
    public static partial void LogDiscordStartupMessage(
        this ILogger<DiscordBotApplication> logger,
        string message);

    [LoggerMessage(LogLevel.Warning,
@"\x1b[43mDisconnecting from Discord Gateway\x1b[0m
Triggered by:{@disconnectedBy}
Reason:{@reason}")]
    public static partial void LogDisconnect(
        this ILogger<DiscordBotApplication> logger,
        string reason,
        string disconnectedBy = "Gateway");
}
