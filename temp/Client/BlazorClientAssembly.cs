//using System.Diagnostics;
//using System.Reflection;
//using System.Runtime.CompilerServices;

//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

//using MudBlazor.Services;

//[assembly: InternalsVisibleTo("BlazorClient.UnitTests")]
//namespace BlazorClient;
///// <summary> Class to reference the Client <see cref="Assembly"/> </summary>
//public static class BlazorClientAssembly
//{
//    /// <summary> A Reference to the Client <see cref="Assembly"/> </summary>
//    public static Assembly Reference => typeof(BlazorClientAssembly).Assembly;

//    /// <summary>Registers Blazor Client Shared services.</summary>
//    /// <param name="builder">The application builder</param>
//    /// <returns>The <see cref="WebAssemblyHostBuilder"/> for chained invocation</returns>
//    public static WebAssemblyHostBuilder AddBlazorClientServices(this WebAssemblyHostBuilder builder)
//    {
//        builder.Services.AddScoped(_ =>
//        new HttpClient
//        {
//            BaseAddress = new Uri(builder.Configuration.GetValue<string>("Hosting:ServerAddress")
//                    ?? throw new UnreachableException("Invalid Configuration"))
//        })
//            .AddMudServices(configuration =>
//            {
//                configuration.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
//                configuration.SnackbarConfiguration.HideTransitionDuration = 100;
//                configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
//                configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
//                configuration.SnackbarConfiguration.ShowCloseIcon = false;
//            });

//        builder.Services.AddScoped<ChampionService>();

//        return builder;
//    }
//}
