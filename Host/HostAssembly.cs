using System.Reflection;
using System.Runtime.CompilerServices;

using Application;

using Carter;

using Domain.Users;

using Host.Client;
using Host.Components;
using Host.Components.Account;

using Infrastructure;
using Infrastructure.Interceptors;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Presentation.Discord;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

[assembly: InternalsVisibleTo("Host.UnitTests")]

namespace Host;

/// <summary>Class to reference the Host <see cref="Assembly"/> </summary>
public static class HostAssembly
{
    /// <summary>A Reference to the Host <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(HostAssembly).Assembly;

    /// <summary>Configures the application pipeline with various middleware components</summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplication"/> instance.</returns>
    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseDatabase();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(ClientAssembly.Reference);

        app.MapAdditionalIdentityEndpoints();
        app.MapHub<TestHub>("test-notifications");
        app.MapCarter();

        app.UseDiscord();

        app.UseCors(policy => policy //TODO
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

        return app;
    }

    /// <summary>Configures services for the application</summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance used to configure services.</param>
    /// <returns>The configured <see cref="WebApplication"/> instance.</returns>
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddRazorServices()
            .AddIdentityServices()
            .AddDatabaseServices()
            .AddApplicationServices()
            .AddDiscordApplication()
            .AddBlazorClientSharedServices()
            .Services
                .AddCors()
                .AddCarter()
                .AddHostedService<TestBackgroundService>()
                .AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        return builder.Build();
    }

    // TODO
    /// <summary>Retrieves a console theme for Serilog console logging</summary>
    /// <returns>An instance of <see cref="AnsiConsoleTheme"/>.</returns>
    internal static AnsiConsoleTheme GetConsoleTheme() => new(new Dictionary<ConsoleThemeStyle, string>
    {
        [ConsoleThemeStyle.Text] = "\x1b[0m",
        [ConsoleThemeStyle.SecondaryText] = "\x1b[90m",
        [ConsoleThemeStyle.TertiaryText] = "\x1b[90m",
        [ConsoleThemeStyle.Invalid] = "\x1b[31m",
        [ConsoleThemeStyle.Null] = "\x1b[95m",
        [ConsoleThemeStyle.Name] = "\x1b[93m",
        [ConsoleThemeStyle.String] = "\x1b[96m",
        [ConsoleThemeStyle.Number] = "\x1b[95m",
        [ConsoleThemeStyle.Boolean] = "\x1b[95m",
        [ConsoleThemeStyle.Scalar] = "\x1b[95m",
        [ConsoleThemeStyle.LevelVerbose] = "\x1b[34m",
        [ConsoleThemeStyle.LevelDebug] = "\x1b[90m",
        [ConsoleThemeStyle.LevelInformation] = "\x1b[36m",
        [ConsoleThemeStyle.LevelWarning] = "\x1b[43m",
        [ConsoleThemeStyle.LevelError] = "\x1b[31m",
        [ConsoleThemeStyle.LevelFatal] = "\x1b[37;41m"
    });

    private static WebApplicationBuilder AddDatabaseServices(this WebApplicationBuilder builder)
    {
        builder.AddInfrastructureServices();

        string connectionString = builder.Configuration.GetConnectionString("ApplicationSqlServer")
            ?? throw new InvalidOperationException("Connection string 'ApplicationSqlServer' not found.");

        builder.Services.AddSingleton(new ReadConnectionString(connectionString)); // Dapper reads in queries
        builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) => // Ef writes in commands
        {
            SaveChangesInterceptor[] saveChangesInterceptor = [
                serviceProvider.GetRequiredService<UpdateTimeStampsInterceptor>()
            ];

            options.UseSqlServer(connectionString);
            options.AddInterceptors(saveChangesInterceptor);

            if (builder.Environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        return builder;
    }

    private static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies();

        return builder;
    }

    private static WebApplicationBuilder AddRazorServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        return builder;
    }
}
