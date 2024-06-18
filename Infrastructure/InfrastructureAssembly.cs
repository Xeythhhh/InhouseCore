using System.Reflection;
using System.Runtime.CompilerServices;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

[assembly: InternalsVisibleTo("Infrastructure.UnitTests")]
namespace Infrastructure;
/// <summary> Class to reference the Infrastructure <see cref="Assembly"/> </summary>
public static class InfrastructureAssembly
{
    /// <summary> A Reference to the Infrastructure <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(InfrastructureAssembly).Assembly;

    public static IHostApplicationBuilder AddEntityFrameworkServices(this IHostApplicationBuilder builder)
    {
        Id.RegisterConverters();
        Log.Debug("Value Converters: Key -> Converter used<TId, TEntity>\n{@DebugInfo}"
            , string.Join("\n",
            Id.ValueConverters.Select(c =>
            $"{c.Key.Name} -> <{string.Join(", ", c.Value.GetType().GenericTypeArguments.Select(t => t.Name))}>")));
        Id.RegisterGenerator(builder.Configuration.GetValue<int>("IdGen:EfCore"));

        return builder;
    }

    public static void UseDatabase(this IHost app)
    {
        app.Services.EnsureDatabaseMigrated();
        //app.Services.UseDatabaseSeed();
    }

    private static void UseDatabaseSeed(this IServiceProvider serviceProvider) => throw new NotImplementedException();

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
