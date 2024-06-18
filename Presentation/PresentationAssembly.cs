using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Presentation.Discord;
using Presentation.Discord.Configuration;

[assembly: InternalsVisibleTo("Presentation.UnitTests")]
namespace Presentation;

/// <summary> Class to reference the Presentation <see cref="Assembly"/> </summary>
public static class PresentationAssembly
{
    /// <summary> A Reference to the Presentation <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(PresentationAssembly).Assembly;

    public static IHostApplicationBuilder AddDiscordBotApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<DiscordApplicationConfiguration>(builder.Configuration.GetSection("Discord"));

        builder.Services.AddSingleton<DiscordBotApplication>();
        return builder;
    }

    public static void UseDiscord(this IHost app)
    {
        DiscordBotApplication discordBotApplication = app.Services.GetRequiredService<DiscordBotApplication>();

        discordBotApplication.Connect();
        discordBotApplication.Disconnect("Test");
    }
}