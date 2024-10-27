using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;

using Domain.Champions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("Domain.UnitTests")]
namespace Domain;
/// <summary>Class to reference the Domain <see cref="Assembly"/></summary>
public static class DomainAssembly
{
    /// <summary>A Reference to the Domain <see cref="Assembly"/></summary>
    public static Assembly Reference => typeof(DomainAssembly).Assembly;

    /// <summary>Registers Entity Framework services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    /// <exception cref="ConfigurationErrorsException"></exception>
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation.</returns>
    private static IHostApplicationBuilder InitializeDomainObjects(this IHostApplicationBuilder builder)
    {
        Champion.ChampionRole.ConfigureValidValues(builder.Configuration.GetSection("Domain:Roles").Get<string[]>());
        Champion.Augment.AugmentTarget.ConfigureValidValues(builder.Configuration.GetSection("Domain:AugmentTargets").Get<string[]>());

        return builder;
    }

    /// <summary>Registers Domain services for the application.</summary>
    /// <param name="builder">The host application builder.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation.</returns>
    public static IHostApplicationBuilder AddDomainServices(this IHostApplicationBuilder builder)
    {
        builder.InitializeDomainObjects();

        return builder;
    }
}