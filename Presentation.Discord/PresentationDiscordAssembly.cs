using System.Reflection;
using System.Runtime.CompilerServices;

using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Presentation.Discord.Configuration;
using Presentation.Discord.Exceptions;

[assembly: InternalsVisibleTo("Presentation.Discord.UnitTests")]
namespace Presentation.Discord;

/// <summary> Class to reference the Presentation.Discord <see cref="Assembly"/> </summary>
public static class PresentationDiscordAssembly
{
    /// <summary> A Reference to the Presentation.Discord <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(PresentationDiscordAssembly).Assembly;

    /// <summary>Adds Discord application services to the <see cref="IHostApplicationBuilder"/></summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to add services to.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/>  for chained invocation.</returns>
    public static IHostApplicationBuilder AddDiscordApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<Configuration.DiscordConfiguration>();
        string? token = builder.Configuration.GetDiscordGatewayToken();

        builder.Services.AddDiscordClient(token, DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents)
            .ConfigureEventHandlers(eventHandlingBuilder =>
                eventHandlingBuilder.HandleGuildDownloadCompleted(DiscordClientEvents.OnGuidDownloadCompleted));

        return builder;
    }

    /// <summary> Use Discord services/>.</summary>
    /// <param name="app">The <see cref="IHost"/> to use Discord services with.</param>
    public static void UseDiscord(this IHost app)
    {
        DiscordClient discordClient = app.Services.GetRequiredService<DiscordClient>();
        DiscordOptions options = app.Services.GetRequiredService<IOptions<DiscordOptions>>().Value;
        discordClient.UseCommands(options.CommandsConfiguration);

        discordClient.ConnectAsync();
    }

    /// <summary> Gets the Discord gateway token from the configuration.</summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> to get the token from.</param>
    /// <returns>The Discord gateway token.</returns>
    /// <exception cref="DiscordConfigurationException">Thrown when the Discord token is missing.</exception>
    private static string GetDiscordGatewayToken(this IConfiguration configuration)
    {
        string? token = configuration.GetValue<string>("Discord:Token");
        return string.IsNullOrEmpty(token)
            ? throw new MissingDiscordGatewayTokenException()
            : token;
    }
}