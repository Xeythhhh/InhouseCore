using CSharpFunctionalExtensions;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure;
public static class HostingExtensions
{
    public static IHostApplicationBuilder AddEntityFrameworkServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<IdValueGeneratorOptions>(builder.Configuration.GetSection("IdGen:EfCore"));
        builder.Services.AddSingleton<IdConverters>();

        return builder;
    }

    public static void UseDatabase(this IHost app)
    {
        Result valueGeneratorResult = app.Services.EnsureValueGeneratorsInitialized();
        if (valueGeneratorResult.IsFailure) throw new InvalidOperationException(valueGeneratorResult.Error);

        app.Services.EnsureDatabaseMigrated();
        //app.Services.UseDatabaseSeed();
    }

#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable RCS1213 // Remove unused member declaration
    private static void UseDatabaseSeed(this IServiceProvider serviceProvider)
#pragma warning restore RCS1213 // Remove unused member declaration
#pragma warning restore IDE0051 // Remove unused private members
    {
#pragma warning disable IDE0022 // Use expression body for method
        throw new NotImplementedException();
#pragma warning restore IDE0022 // Use expression body for method
    }

    private static Result EnsureValueGeneratorsInitialized(this IServiceProvider serviceProvider)
        => IdValueGenerator.RegisterGenerator(
            serviceProvider.GetRequiredService<IdValueGeneratorOptions>());

    private static void EnsureDatabaseMigrated(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!dbContext.Database.GetPendingMigrations().Any()) return;

        scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>()
            .LogInformation("Migrating Database");

        dbContext.Database.Migrate();
        dbContext.SaveChanges();
    }
}

internal class IdValueGeneratorOptions
{
    public int GeneratorId { get; set; }
}