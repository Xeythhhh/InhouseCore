using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("Application.UnitTests")]
namespace Application;
/// <summary> Class to reference the Application <see cref="Assembly"/> </summary>
public static class ApplicationAssembly
{
    /// <summary> A Reference to the Application <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ApplicationAssembly).Assembly;

    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Reference));

        return builder;
    }
}