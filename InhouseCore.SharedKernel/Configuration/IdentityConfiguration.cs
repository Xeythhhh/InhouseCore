using Microsoft.Extensions.Configuration;

namespace InhouseCore.SharedKernel.Configuration;

/// <summary>
/// Configuration object for Authentication and Authorization
/// </summary>
public static class IdentityConfiguration
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static IConfiguration _configuration;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Registers the <see cref="IConfiguration"/> object.
    /// </summary>
    /// <param name="configuration"></param>
    internal static void Setup(IConfiguration configuration) => _configuration = configuration;


    //public static string ApplicationName => _configuration.GetValue<string>();

    /// <summary>
    /// Shared Authentication schemes
    /// </summary>
    public static class AuthenticationSchemes
    {
        public static string Default => "Internal";
    }

    /// <summary>
    /// Configuration object for Jwt
    /// </summary>
    internal static class Jwt
    {
        internal static string Issuer => _configuration.GetValue<string>("Jwt:Issuer")
            ?? throw new InvalidOperationException($"Jwt '{nameof(Issuer)}' not found.");
        internal static string Audience => _configuration.GetValue<string>("Jwt:Audience")
            ?? throw new InvalidOperationException($"Jwt '{nameof(Audience)}' not found.");
        internal static string EncryptionKey => _configuration.GetValue<string>("Jwt:EncryptionKey")
            ?? throw new InvalidOperationException($"Jwt '{nameof(EncryptionKey)}' not found.");
    }

    /// <summary>
    /// Configuration object for Discord OAuth <see href="https://www.nuget.org/packages/AspNet.Security.OAuth.Discord">Library</see> <see href="https://discord.com/developers/docs/topics/oauth2">Docs</see>
    /// </summary>
    internal static class Discord
    {
        internal static string AuthorizationEndpoint => _configuration.GetValue<string>("Discord:AuthorizationEndpoint")
            ?? throw new InvalidOperationException($"Discord '{nameof(AuthorizationEndpoint)}' not found.");
        internal static string TokenEndpoint => _configuration.GetValue<string>("Discord:TokenEndpoint")
            ?? throw new InvalidOperationException($"Discord '{nameof(TokenEndpoint)}' not found.");
        internal static string UserInformationEndpoint => _configuration.GetValue<string>("Discord:UserInformationEndpoint")
            ?? throw new InvalidOperationException($"Discord '{nameof(UserInformationEndpoint)}' not found.");
        internal static string OAuthCallback => _configuration.GetValue<string>("Discord:OAuthCallback")
            ?? throw new InvalidOperationException($"Discord '{nameof(OAuthCallback)}' not found.");
        internal static string ClientId => _configuration.GetValue<string>("Discord:ApplicationId")
            ?? throw new InvalidOperationException($"Discord '{nameof(ClientId)}' not found.");
        internal static string ClientSecret => _configuration.GetValue<string>("Discord:ClientSecret")
            ?? throw new InvalidOperationException($"Discord '{nameof(ClientSecret)}' not found.");

    }
}
