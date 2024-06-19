using DSharpPlus.Commands;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Presentation.Discord.Configuration;

internal sealed class CommandsConfigurationOptions(IConfiguration configuration)
    : IConfigureOptions<CommandsConfiguration>
{
    private const string ConfigurationSection = "Discord:CommandsConfiguration";

    public void Configure(CommandsConfiguration options) =>
        configuration.GetSection(ConfigurationSection).Bind(options);
}