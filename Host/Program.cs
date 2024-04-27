using System.Globalization;

using Host;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

//Logger used during bootstrap, this is replaced further down the pipeline
const string logFormat = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: logFormat,
        formatProvider: CultureInfo.InvariantCulture,
        theme: HostingExtensions.GetConsoleTheme())
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((builderContext, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(builderContext.Configuration)
        .WriteTo.Console(
                outputTemplate: logFormat,
                formatProvider: CultureInfo.InvariantCulture,
                theme: HostingExtensions.GetConsoleTheme())
        .Enrich.FromLogContext());

    var app = builder
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