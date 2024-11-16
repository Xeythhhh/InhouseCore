using System.Globalization;

using SharedKernel;

using Serilog;
using Api;

//Logger used during bootstrap, this is replaced further down the pipeline
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: AppConstants.LogFormat,
        formatProvider: CultureInfo.InvariantCulture,
        theme: ApiAssembly.GetConsoleTheme())
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((builderContext, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(builderContext.Configuration)
        .WriteTo.Console(
                outputTemplate: AppConstants.LogFormat,
                formatProvider: CultureInfo.InvariantCulture,
                theme: ApiAssembly.GetConsoleTheme())
        .Enrich.FromLogContext());

    builder
        .ConfigureServices()
        .ConfigurePipeline()
        .Run();
}
catch (Exception exception) when (exception is not HostAbortedException)
{
    Log.Fatal(exception, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete.");
    Log.CloseAndFlush();
}