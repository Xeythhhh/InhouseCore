namespace Presentation.Discord.Exceptions;

internal class DiscordConfigurationException : Exception
{
    public DiscordConfigurationException() { }
    public DiscordConfigurationException(string? message) : base(message) { }
    public DiscordConfigurationException(string? message, Exception? innerException) : base(message, innerException) { }

    internal static DiscordConfigurationException NoGuilds() => new DiscordConfigurationException("Discord bot application is not part of any guild");
    internal static DiscordConfigurationException NoChannels() => new DiscordConfigurationException("Discord bot application does not have access to any channels in this guild");
    internal static DiscordConfigurationException MissingTokenException() => new DiscordConfigurationException(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
            ? "Missing Discord Token, it should be stored as a user secret."
            : "Missing Discord Token");
}