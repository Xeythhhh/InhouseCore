using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Presentation.Discord;
using Presentation.Discord.Configuration;

namespace Presentation;
public static class HostingExtensions
{
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
