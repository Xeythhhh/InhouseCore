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

    /// <summary> Adds shared configuration settings from JSON files.
    /// Specifically, it looks for <c>sharedsettings.json</c> and <c>sharedsettings.{environment}.json</c>.</summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to which the shared settings will be added.</param>
    /// <returns>The updated <see cref="IHostApplicationBuilder"/> instance.</returns>
    public static IHostApplicationBuilder AddSharedSettings(this IHostApplicationBuilder builder)
    {
        // Calculate relative path to sharedsettings.json based on assembly location
        string basePath = builder.Environment.ContentRootPath;
        string environment = builder.Environment.EnvironmentName;
        string sharedSettingsPath = Path.Combine(basePath, "..", "SharedKernel", "sharedsettings.json");
        string sharedSettingsEnvironmentPath = Path.Combine(basePath, "..", "SharedKernel", $"sharedsettings.{environment}.json");

        if (File.Exists(sharedSettingsPath))
            builder.Configuration.AddJsonFile(sharedSettingsPath, optional: false, reloadOnChange: true);

        if (File.Exists(sharedSettingsEnvironmentPath))
            builder.Configuration.AddJsonFile(sharedSettingsEnvironmentPath, optional: true, reloadOnChange: true);

        return builder;
    }

    /// <summary>Adds shared configuration settings from JSON files.
    /// Specifically, it looks for <c>sharedsettings.json</c> and <c>sharedsettings.{environment}.json</c>.</summary>
    /// <param name="builder">The <see cref="WebAssemblyHostBuilder"/> to which the shared settings will be added.</param>
    /// <returns>The updated <see cref="WebAssemblyHostBuilder"/> instance.</returns>
    public static async Task<WebAssemblyHostBuilder> AddSharedSettings(this WebAssemblyHostBuilder builder)
    {
        using HttpClient httpClient = new()
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        };

        using HttpResponseMessage response = await httpClient.GetAsync("sharedsettings.json");
        using HttpResponseMessage responseEnvironment = await httpClient.GetAsync($"sharedsettings.{builder.HostEnvironment.Environment}.json");
        await using Stream stream = await response.Content.ReadAsStreamAsync();
        await using Stream streamEnvironment = await responseEnvironment.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);
        builder.Configuration.AddJsonStream(streamEnvironment);

        //!!Blazor will not serve files that are not included in the project so if your build action copies
        //files to the wwwroot published folder, it will not be served, include the file in the blazor project

        return builder;
    }
}