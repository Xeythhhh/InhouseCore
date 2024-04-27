using Domain.Entities.Users;

using Infrastructure.Converters.Ids;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using static System.Formats.Asn1.AsnWriter;

namespace Infrastructure;
public static class HostingExtensions
{
    public static IServiceCollection AddValueConverters(this IServiceCollection services)
    {
        services.AddSingleton<ValueConverter<ApplicationUserId, string>, ApplicationUserIdConverter>();

        return services;
    }

    public static void UseDatabase(this IHost app)
    {
        app.Services.EnsureDatabaseMigrated();
        //app.Services.UseDatabaseSeed();
    }

    private static void UseDatabaseSeed(this IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }

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
