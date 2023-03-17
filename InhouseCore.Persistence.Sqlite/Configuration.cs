using Microsoft.Extensions.Configuration;

namespace InhouseCore.Persistence.Sqlite;

/// <summary>
/// Configuration object for <see cref="PersistenceSqliteAssembly"/>.
/// </summary>
internal static class Configuration
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static IConfiguration _configuration;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
}
