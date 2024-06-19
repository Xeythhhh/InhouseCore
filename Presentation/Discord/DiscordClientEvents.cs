using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

using Presentation.Discord.Exceptions;

namespace Presentation.Discord;
public static class DiscordClientEvents
{
    internal static Task OnGuidDownloadCompleted(DiscordClient _, GuildDownloadCompletedEventArgs args)
    {
        DiscordGuild guild = args.Guilds.Select(g => g.Value)
            .FirstOrDefault()
                ?? throw DiscordConfigurationException.NoGuilds();

        DiscordChannel channel = guild.Channels.Select(c => c.Value)
            .FirstOrDefault(c => !c.IsCategory)
                ?? throw DiscordConfigurationException.NoChannels();

        channel.SendMessageAsync("123");

        return Task.CompletedTask;
    }

    //todo
    //public static DiscordClient DiscordClient { get; set; }

    //public static Task Connect()
    //{
    //    _logger.LogInformation("Discord Client Connecting to Gateway");
    //    return DiscordClient.ConnectAsync();
    //}

    //public static Task Disconnect(string reason, string disconnectedBy = "Host")
    //{
    //    _logger.LogDisconnect(reason, disconnectedBy);
    //    return DiscordClient.DisconnectAsync();
    //}
}
