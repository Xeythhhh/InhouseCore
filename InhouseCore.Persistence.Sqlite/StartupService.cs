using InhouseCore.Domain.Entities.Identity;
using InhouseCore.Persistence.Sqlite.Converters;

using Microsoft.AspNetCore.Authentication;
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
        Config.Setup(configuration);

        services.AddSingleton<ValueConverter<Ulid, string>, UlidToStringConverter>();
        services.AddDbContext<InhouseCoreDbContext>(options =>
        options.UseSqlite(Config.SqliteDatabase.ConnectionString, sqliteOptions =>
            sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
            services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddRepositories();
    }

    /// <summary>
    /// Registeres and configures Authentication and Authorization services.
    /// </summary>
    /// <param name="services"></param>
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<InhouseCoreDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<User, InhouseCoreDbContext>();

        services.AddAuthentication().AddIdentityServerJwt();
    }

    /// <summary>
    /// Executes all pending migrations on <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> .</param>
    public static void MigrateSqliteDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
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
