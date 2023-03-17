using InhouseCore.Persistence.Sqlite.Converters;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InhouseCore.Persistence.Sqlite;

/// <summary>
/// Startup Routine for <see cref="PersistenceSqliteAssembly"/>.
/// </summary>
public static class StartupService
{
    /// <summary>
    /// Registers and configures <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    public static void AddPersistenceSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        Configuration.Setup(configuration);

        services.AddSingleton<ValueConverter<Ulid, string>, UlidToStringConverter>();
        services.AddDbContext<InhouseCoreDbContext>(options =>
            options.UseSqlite(Configuration.Data.SqliteConnectionString, sqliteOptions =>
                sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
            services.AddDatabaseDeveloperPageExceptionFilter();

        services.MigrateSqliteDatabase();

        services.AddRepositories();
    }

    /// <summary>
    /// Executes all pending migrations on <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> .</param>
    private static void MigrateSqliteDatabase(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<InhouseCoreDbContext>();
        
        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();
    }

    /// <summary>
    /// Registers all repositories to DI.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> .</param>
    private static void AddRepositories(this IServiceCollection services)
    {
        var assemblyTypes = PersistenceSqliteAssembly.Value.GetTypes();
        var repositoryTypes = assemblyTypes
            .Where(x => x.Name.EndsWith("Repository"))
            .Where(x => !x.IsAbstract);

        foreach (var implementationType in repositoryTypes)
        {
            services.AddTransient(implementationType);
            foreach (var serviceType in implementationType.GetInterfaces())
                services.AddTransient(serviceType, implementationType);
        }
    }
}
