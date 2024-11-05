using DSharpPlus.Commands;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using SharedKernel;

namespace Presentation.Discord.Configuration;

/// <summary>Configuration class for Discord-related options.</summary>
/// <param name="configuration">The IConfiguration instance.</param>
internal sealed class DiscordApplicationConfiguration(IConfiguration configuration) :
    IConfigureOptions<DiscordOptions>
{
    /// <summary>Configures the DiscordOptions instance by binding it to the 'Discord' section of IConfiguration.</summary>
    /// <param name="options">The DiscordOptions instance to configure.</param>
    public void Configure(DiscordOptions options) =>
        configuration.GetSection(AppConstants.Configuration.Discord.ConfigurationSection).Bind(options);
}

/// <summary>Options class representing Discord-related configuration.</summary>
internal sealed class DiscordOptions
{
    /// <summary>Gets the configuration for Discord commands.</summary>
    public CommandsConfiguration CommandsConfiguration { get; }
}
