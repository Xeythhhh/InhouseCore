using Presentation.Discord.Exceptions;

namespace Presentation.Discord.Configuration;

public class DiscordApplicationConfiguration
{
    public ulong DebugGuildId { get; set; }

    private string? _token;
    public string Token
    {
        get => _token ?? throw DiscordConfigurationException.MissingTokenException();
        set => _token = value;
    }
}
