using System.Reflection;
using System.Runtime.CompilerServices;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("Infrastructure.UnitTests")]
namespace Infrastructure;

/// <summary>Class to reference the Infrastructure <see cref="Assembly"/>.</summary>
public static class InfrastructureAssembly
{
    /// <summary>A Reference to the Infrastructure <see cref="Assembly"/>.</summary>
    public static Assembly Reference => typeof(InfrastructureAssembly).Assembly;

    /// <summary>Registers Entity Framework services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    public static IHostApplicationBuilder AddEntityFrameworkServices(this IHostApplicationBuilder builder)
    {
        Id.RegisterConverters();
        Id.RegisterGenerator(builder.Configuration.GetValue<int>("IdGen:EfCore"));
        return builder;
    }

    /// <summary>Configures database usage for the application.</summary>
    /// <param name="app">The host application.</param>
    public static void UseDatabase(this IHost app)
    {
        app.Services.EnsureDatabaseMigrated();
        //app.Services.UseDatabaseSeed();
    }

    /// <summary>Seeds the database with initial data.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    private static void UseDatabaseSeed(this IServiceProvider serviceProvider) => throw new NotImplementedException();

    /// <summary>Ensures that the database migrations are applied.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    private static void EnsureDatabaseMigrated(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!dbContext.Database.GetPendingMigrations().Any()) return;

        scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>()
            .LogInformation("Migrating Database");

        dbContext.Database.Migrate();
        dbContext.SaveChanges();
    }
}
