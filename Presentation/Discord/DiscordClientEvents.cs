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
                ?? throw new NoDiscordGuildsException();

        DiscordChannel channel = guild.Channels.Select(c => c.Value)
            .FirstOrDefault(c => !c.IsCategory)
                ?? throw new NoDiscordChannelsException();

        channel.SendMessageAsync("123");

        return Task.CompletedTask;
    }
}
