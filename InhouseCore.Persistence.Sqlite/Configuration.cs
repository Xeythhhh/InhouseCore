using Microsoft.Extensions.Configuration;

namespace InhouseCore.Persistence.Sqlite;

/// <summary>
/// Configuration object for <see cref="PersistenceSqliteAssembly"/>.
/// </summary>
internal static class Configuration
{
    private static IConfiguration _configuration;

    /// <summary>
    /// Registers the <see cref="IConfiguration"/> object.
    /// </summary>
    /// <param name="configuration"></param>
    internal static void Setup(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Configuration object for <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    internal static class Data
    {
        internal static string SqliteConnectionString => _configuration.GetConnectionString(nameof(SqliteConnectionString)) ?? throw new InvalidOperationException($"Connection string '{nameof(SqliteConnectionString)}' not found.");
    }

    internal static class Identity
    {
        internal static class Jwt
        {

            internal static string Issuer => _configuration.GetValue<string>("Jwt:Issuer")
                ?? throw new InvalidOperationException($"Jwt '{nameof(Issuer)}' not found.");
            internal static string Audience => _configuration.GetValue<string>("Jwt:Audience") 
                ?? throw new InvalidOperationException($"Jwt '{nameof(Audience)}' not found.");
            internal static string EncryptionKey => _configuration.GetValue<string>("Jwt:EncryptionKey") 
                ?? throw new InvalidOperationException($"Jwt '{nameof(EncryptionKey)}' not found.");
        }

        internal static class Discord
        {
            internal static string AuthorizationEndpoint => _configuration.GetValue<string>("Discord:AuthorizationEndpoint")
                ?? throw new InvalidOperationException($"Discord '{nameof(AuthorizationEndpoint)}' not found.");
            internal static string TokenEndpoint => _configuration.GetValue<string>("Discord:TokenEndpoint")
                ?? throw new InvalidOperationException($"Discord '{nameof(TokenEndpoint)}' not found.");
            internal static string UserInformationEndpoint => _configuration.GetValue<string>("Discord:UserInformationEndpoint")
                ?? throw new InvalidOperationException($"Discord '{nameof(UserInformationEndpoint)}' not found.");
            internal static string ClientId => _configuration.GetValue<string>("Discord:ApplicationId")
                ?? throw new InvalidOperationException($"Discord '{nameof(ClientId)}' not found.");
            internal static string ClientSecret => _configuration.GetValue<string>("Discord:ClientSecret")
                ?? throw new InvalidOperationException($"Discord '{nameof(ClientSecret)}' not found.");

        }
    }
}
