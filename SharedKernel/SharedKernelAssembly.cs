using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("SharedKernel.UnitTests")]
namespace SharedKernel;
/// <summary> Class to reference the SharedKernel <see cref="Assembly"/> </summary>
public static class SharedKernelAssembly
{
    /// <summary> A Reference to the SharedKernel <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(SharedKernelAssembly).Assembly;

    /// <summary>
    /// Adds shared configuration settings from JSON files located in the same directory as the assembly.
    /// Specifically, it looks for <c>sharedsettings.json</c> and <c>sharedsettings.{environment}.json</c>.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to which the shared settings will be added.</param>
    /// <returns>The updated <see cref="IHostApplicationBuilder"/> instance.</returns>
    public static IHostApplicationBuilder AddSharedSettings(this IHostApplicationBuilder builder)
    {
        AddSharedSettingsCore(builder.Configuration);
        return builder;
    }

    /// <summary>
    /// Adds shared configuration settings from JSON files located in the same directory as the assembly.
    /// Specifically, it looks for <c>sharedsettings.json</c> and <c>sharedsettings.{environment}.json</c>.
    /// </summary>
    /// <param name="builder">The <see cref="WebAssemblyHostBuilder"/> to which the shared settings will be added.</param>
    /// <returns>The updated <see cref="WebAssemblyHostBuilder"/> instance.</returns>
    public static WebAssemblyHostBuilder AddSharedSettings(this WebAssemblyHostBuilder builder)
    {
        AddSharedSettingsCore(builder.Configuration);
        return builder;
    }

    /// <summary> Core method to add shared settings to the configuration.</summary>
    /// <param name="configuration">The <see cref="IConfigurationBuilder"/> to which the shared settings will be added.</param>
    private static void AddSharedSettingsCore(IConfigurationBuilder configuration)
    {
        // Retrieve the environment variable for the environment name
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        // Load configuration files
        string assemblyPath = Path.GetDirectoryName(Reference.Location)!;
        string sharedSettingsPath = Path.Combine(assemblyPath, "sharedsettings.json");
        string sharedSettingsEnvironmentPath = Path.Combine(assemblyPath, $"sharedsettings.{environment}.json");

        if (File.Exists(sharedSettingsPath))
            configuration.AddJsonFile(sharedSettingsPath, optional: false, reloadOnChange: true);

        if (File.Exists(sharedSettingsEnvironmentPath))
            configuration.AddJsonFile(sharedSettingsEnvironmentPath, optional: true, reloadOnChange: true);
    }
}