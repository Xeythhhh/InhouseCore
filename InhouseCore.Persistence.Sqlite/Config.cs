using Microsoft.Extensions.Configuration;

namespace InhouseCore.Persistence.Sqlite;

/// <summary>
/// Configuration object for <see cref="PersistenceSqliteAssembly"/>.
/// </summary>
internal static class Config
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
    internal static class SqliteDatabase
    {
        internal static string ConnectionString => _configuration.GetConnectionString("SqliteConnectionString");
    }
}
