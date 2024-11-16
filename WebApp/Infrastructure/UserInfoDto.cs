using SharedKernel.Primitives.Result;

namespace WebApp.Infrastructure;

// Add properties to this class and update the server and client AuthenticationStateProviders
// to expose more information about the authenticated user to the client.
public class UserInfoDto
{
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public required string DiscordId { get; set; }
    public required string DiscordName { get; set; }
    public required string DiscordAvatar { get; set; }
    public required string DiscordVerified { get; set; }

    public static Result<UserInfoDto> Create(
        string userId,
        string email,
        string discordId,
        string discordName,
        string discordAvatar,
        string discordVerified) =>
        new UserInfoDto
        {
            UserId = userId,
            Email = email,
            DiscordId = discordId,
            DiscordName = discordName,
            DiscordAvatar = discordAvatar,
            DiscordVerified = discordVerified
        };
}
