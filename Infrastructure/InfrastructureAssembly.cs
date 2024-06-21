using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;

using Domain.Champions;

using FluentResults;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SharedKernel.Extensions;

[assembly: InternalsVisibleTo("Infrastructure.UnitTests")]
namespace Infrastructure;

/// <summary>Class to reference the Infrastructure <see cref="Assembly"/>.</summary>
public static class InfrastructureAssembly
{
    /// <summary>A Reference to the Infrastructure <see cref="Assembly"/>.</summary>
    public static Assembly Reference => typeof(InfrastructureAssembly).Assembly;

    /// <summary>Registers Entity Framework services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    /// <exception cref="ConfigurationErrorsException"></exception>
    public static IHostApplicationBuilder AddEntityFrameworkServices(this IHostApplicationBuilder builder)
    {
        Result registerConvertersResult = Id.RegisterConverters();
        if (registerConvertersResult.IsFailed) throw new ConfigurationErrorsException(registerConvertersResult.GetErrorMessage());

        Id.RegisterGeneratorId(builder.Configuration.GetValue<int>("IdGen:EfCore"));

        return builder;
    }

    /// <summary>Configures database usage for the application.</summary>
    /// <param name="app">The host application.</param>
    public static void UseDatabase(this IHost app)
    {
        app.Services.EnsureDatabaseMigrated();
        app.Services.UseDatabaseSeed();
    }

    /// <summary>Seeds the database with initial data.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The <see cref="IServiceProvider"/> for chained invocation.</returns>
    private static IServiceProvider UseDatabaseSeed(this IServiceProvider serviceProvider)
    {
        Console.WriteLine("Implement Database Seed ");

        return serviceProvider;
    }

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
