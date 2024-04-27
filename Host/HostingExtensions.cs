using Domain.Entities.Users;

using FluentAssertions.Common;

using Host.Client;
using Host.Components;
using Host.Components.Account;

using Infrastructure;
using Infrastructure.Converters;
using Infrastructure.Converters.Ids;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Presentation.Discord;
using Presentation.Discord.Configuration;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Host;
internal static class HostingExtensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ValueConverter<ApplicationUserId, string>, ApplicationUserIdConverter>();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

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

        string connectionString = builder.Configuration.GetConnectionString("ApplicationSqlServer")
            ?? throw new InvalidOperationException("Connection string 'ApplicationSqlServer' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        builder.Services.Configure<DiscordApplicationConfiguration>(builder.Configuration.GetSection("Discord"));
        builder.Services.AddSingleton<DiscordBotApplication>();

        return builder.Build();
    }

    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        dbContext.SaveChanges();

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

        app.Services.GetRequiredService<DiscordBotApplication>().Connect();
        app.Services.GetRequiredService<DiscordBotApplication>().Disconnect("Test");

        return app;
    }

    // TODO: Implement new AnsiProcessor in SharedKernel
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
}
