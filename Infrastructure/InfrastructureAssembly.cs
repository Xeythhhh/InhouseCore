using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;

using SharedKernel.Primitives.Result;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Domain.Champions;
using System.Text.Json;
using SharedKernel.Primitives.Reasons;
using SharedKernel;
using Domain.ReferenceData;

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
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation.</returns>
    private static IHostApplicationBuilder AddEntityFrameworkServices(this IHostApplicationBuilder builder)
    {
        Result registerConvertersResult = Id.RegisterConverters();
        if (registerConvertersResult.IsFailed) throw new ConfigurationErrorsException(registerConvertersResult.Errors[0].Message); //todo

        Id.RegisterGeneratorId(builder.Configuration.GetValue<int>(AppConstants.Configuration.EfCoreIdGenId));

        return builder;
    }

    /// <summary>Registers Infrastructure services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation.</returns>
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddEntityFrameworkServices();

        builder.Services.Scan(selector =>
            selector.FromAssemblies(Reference)
                .AddClasses()
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        return builder;
    }

    /// <summary> Ensures that the database migrations are applied.</summary>
    /// <param name="app"> The service provider.</param>
    /// <returns> The <see cref="IHost"/> for chained invocation.</returns>
    public static IHost EnsureDatabaseMigrated(this IHost app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!dbContext.Database.GetPendingMigrations().Any()) return app;

        scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>()
            .LogInformation("Migrating Database");

        dbContext.Database.Migrate();
        dbContext.SaveChanges();

        return app;
    }
}
