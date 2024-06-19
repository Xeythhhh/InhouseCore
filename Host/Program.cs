using System.Diagnostics;
using System.Globalization;

using Host;

using Serilog;

//Logger used during bootstrap, this is replaced further down the pipeline
const string logFormat = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: logFormat,
        formatProvider: CultureInfo.InvariantCulture,
        theme: HostAssembly.GetConsoleTheme())
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((builderContext, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(builderContext.Configuration)
        .WriteTo.Console(
                outputTemplate: logFormat,
                formatProvider: CultureInfo.InvariantCulture,
                theme: HostAssembly.GetConsoleTheme())
        .Enrich.FromLogContext());

    WebApplication app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
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