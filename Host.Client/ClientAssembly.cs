using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("Host.Client.UnitTests")]
namespace Host.Client;
/// <summary> Class to reference the Client <see cref="Assembly"/> </summary>
public static class ClientAssembly
{
    /// <summary> A Reference to the Client <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ClientAssembly).Assembly;

    /// <summary>Registers Blazor Client Shared services.</summary>
    /// <param name="builder">The application builder</param>
    /// <returns>The <see cref="WebAssemblyHostBuilder"/> for chained invocation</returns>
    public static WebAssemblyHostBuilder AddBlazorClientSharedServices(this WebAssemblyHostBuilder builder)
    {
        AddBlazorClientSharedServices(builder.Services, builder.Configuration);
        return builder;
    }

    /// <summary>Registers Blazor Client Shared services.</summary>
    /// <param name="builder">The application builder</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> for chained invocation</returns>
    public static IHostApplicationBuilder AddBlazorClientSharedServices(this IHostApplicationBuilder builder)
    {
        AddBlazorClientSharedServices(builder.Services, builder.Configuration);
        return builder;
    }

    private static IServiceCollection AddBlazorClientSharedServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ =>
            new HttpClient
            {
                BaseAddress = new Uri(configuration.GetValue<string>("Hosting:ServerAddress")
                    ?? throw new UnreachableException("Invalid Configuration"))
            });

        services.AddScoped<ChampionService>();

        return services;
    }
}
