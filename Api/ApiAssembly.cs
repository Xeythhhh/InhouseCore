using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

using Api.Components;
using Api.Components.Account;

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

using MudBlazor.Services;

using Serilog.Sinks.SystemConsole.Themes;

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
        app.UseDatabase();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseWebAssemblyDebugging();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
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
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance used to configure services.</param>
    /// <returns>The configured <see cref="WebApplication"/> instance.</returns>
    internal static WebApplication ConfigureServices(this IHostApplicationBuilder builder)
    {
        // TODO
        builder.Services.AddHttpClient("Api",
            client => client.BaseAddress = new Uri(builder.Configuration["ApiAddress"]!));

        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

        // Add MudBlazor services
        builder.Services.AddMudServices();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder
            .AddIdentityServices()
            .AddDomainServices()
            .AddDatabaseServices()
            .AddApplicationServices()
            //.AddDiscordApplication()
            .Services
                //.AddAntiforgery()
                .AddCors()
                .AddCarter()
                .AddSwaggerGen()
                .AddEndpointsApiExplorer()
                .AddHostedService<DemoBackgroundService>()
                .AddSignalR();

        return ((WebApplicationBuilder)builder).Build();
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

    private static IHostApplicationBuilder AddDatabaseServices(this IHostApplicationBuilder builder)
    {
        builder.AddInfrastructureServices();

        string connectionString = builder.Configuration["ConnectionStrings:ApplicationSqlServer"]!;

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
                options.ClientId = builder.Configuration["Discord:AppId"]!;
                options.ClientSecret = builder.Configuration["Discord:AppSecret"]!;
                options.SignInScheme = IdentityConstants.ExternalScheme;

                options.ClaimActions.MapCustomJson("urn:discord:avatar:url", json =>
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                        json.GetString("id"),
                        json.GetString("avatar"),
                        json.GetString("avatar")!.StartsWith("a_") ? "gif" : "png"));

                options.CallbackPath = "/oauth/discord/callback";
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
