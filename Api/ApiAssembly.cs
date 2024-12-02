using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

using Api.Components;
using Api.Components.Account;
using Api.Extensions;
using Api.Services;

using Application;
using Application.Abstractions;

using Carter;

using Domain;
using Domain.Users;

using Infrastructure;
using Infrastructure.Interceptors;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

using MudBlazor.Services;

using Serilog.Sinks.SystemConsole.Themes;

using SharedKernel;

using WebApp;

[assembly: InternalsVisibleTo("Api.UnitTests")]

namespace Api;

/// <summary>Class to reference the Api <see cref="Assembly"/> </summary>
public static class ApiAssembly
{
    /// <summary>A Reference to the Api <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ApiAssembly).Assembly;

    /// <summary>Configures the application pipeline with various middleware components</summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplication"/> instance.</returns>
    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.EnsureDatabaseMigrated();
        app.EnsureGameFoldersParsed();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseWebAssemblyDebugging();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler(AppConstants.ErrorHandlingPath, createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(WebAppAssembly.Reference);

        app.MapAdditionalIdentityEndpoints();

        app.MapHub<TestHub>("test-notifications");
        app.MapCarter();

        //app.UseDiscord();

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
        builder.Services.AddMemoryCache(options =>
        {
            options.SizeLimit = 1024 * 1024 * 50; // Set size limit for cache (50 MB in this case)
            options.CompactionPercentage = 0.2;  // Compact 20% of items when memory pressure is high
        });

        builder.AddHttpClients()
            .Services.AddMudServices()
            .AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder
            .AddIdentityServices()
            .AddDatabaseServices()
            .AddApplicationServices()
            //.AddDiscordApplication()
            .Services
                //.AddAntiforgery()
                .AddCors()
                .AddCarter()
                .AddSwaggerGen()
                .AddEndpointsApiExplorer()
                .AddHostedService<FolderWatcherService>()
                .AddHostedService<DemoBackgroundService>()
                .AddSignalR();

        return builder.Build();
    }

    /// <summary> Configures HTTP clients for the application, registering named clients for both content and API requests.</summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to configure.</param>
    /// <returns>The updated <see cref="WebApplicationBuilder"/>.</returns>
    /// <remarks>
    /// <para /> Registers an unnamed HTTP client for generic content retrieval.
    /// <para /> Registers a named HTTP client for API interactions, setting the <c>BaseAddress</c> from configuration.
    /// <para /> Adds a scoped service for accessing the API client directly using DI.
    /// </remarks>
    private static WebApplicationBuilder AddHttpClients(this WebApplicationBuilder builder)
    {
        // Register a generic content HTTP client
        builder.Services.AddHttpClient(AppConstants.HttpClients.Content);

        // Register a named API HTTP client with a BaseAddress configured from app settings
        builder.Services.AddHttpClient(AppConstants.HttpClients.Api, client =>
            client.BaseAddress = new Uri(builder.Configuration[AppConstants.HttpClients.ApiAddress]!));

        // Provide a scoped service for the named API client
        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient(AppConstants.HttpClients.Api));

        return builder;
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

    /// <summary> Ensures that the game assets are parsed and added to the database.</summary>
    /// <remarks> Call this after database initialization. <seealso cref="InfrastructureAssembly.EnsureDatabaseMigrated(IHost)"/></remarks>
    /// <param name="app"> The <see cref="WebApplication"/>.</param>
    /// <returns> The <see cref="WebApplication"/> for chained invocation.</returns>
    internal static WebApplication EnsureGameFoldersParsed(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ILogger<FolderWatcherService> logger = scope.ServiceProvider.GetRequiredService<ILogger<FolderWatcherService>>();

        string baseFolder = AppContext.BaseDirectory;

        foreach (string gameFolder in Directory.GetDirectories(baseFolder, "Game-*", SearchOption.TopDirectoryOnly))
        {
            FolderWatcherService watcherService = new(logger, scope.ServiceProvider);
            watcherService.ParseGameDataAsync(gameFolder, CancellationToken.None).GetAwaiter().GetResult();
        }

        return app;
    }

    private static IHostApplicationBuilder AddDatabaseServices(this IHostApplicationBuilder builder)
    {
        builder.AddInfrastructureServices();

        string connectionString = builder.Configuration[AppConstants.Configuration.ConnectionStrings.ApplicationSqlServer]!;

        builder.Services.AddSingleton<IReadConnectionString>(new ReadConnectionString(connectionString)); // Dapper reads in queries
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
        }).AddDatabaseDeveloperPageExceptionFilter();

        return builder;
    }

    private static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddDiscord(options =>
            {
                options.ClientId = builder.Configuration[AppConstants.Configuration.Discord.ClientId]!;
                options.ClientSecret = builder.Configuration[AppConstants.Configuration.Discord.ClientSecret]!;
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.SaveTokens = true;

                options.AddScopes(AppConstants.Discord.Scopes.Email,
                    AppConstants.Discord.Scopes.GroupDmJoin,
                    AppConstants.Discord.Scopes.Guilds);

                options.MapClaims(
                    (AppConstants.Discord.Claims.Id, json =>
                        json.GetString(AppConstants.Discord.Claims.Keys.Id)),

                    (AppConstants.Discord.Claims.Username, json =>
                        json.GetString(AppConstants.Discord.Claims.Keys.UserName)),

                    (AppConstants.Discord.Claims.Verified, json =>
                        json.GetString(AppConstants.Discord.Claims.Keys.Verified)),

                    (AppConstants.Discord.Claims.Avatar, json =>
                        string.Format(CultureInfo.InvariantCulture,
                            "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                                  json.GetString(AppConstants.Discord.Claims.Keys.Id),
                                  json.GetString(AppConstants.Discord.Claims.Keys.Avatar),
                                  json.GetString(AppConstants.Discord.Claims.Keys.Avatar)!
                              .StartsWith("a_") ? "gif" : "png")));

                options.CallbackPath = AppConstants.Discord.OAuthCallback;
            })
            .AddIdentityCookies();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        return builder;
    }
}
