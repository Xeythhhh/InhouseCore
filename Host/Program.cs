using Host;

using Serilog;

//Logger used during bootstrap, this is replaced further down the pipeline
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((builderContext, loggerConfig) => loggerConfig
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builderContext.Configuration));

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