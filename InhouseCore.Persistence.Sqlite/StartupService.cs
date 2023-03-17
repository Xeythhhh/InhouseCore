using InhouseCore.Domain.Entities.Identity;
using InhouseCore.Domain.Identity;
using InhouseCore.Persistence.Sqlite.Converters;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InhouseCore.Persistence.Sqlite;

/// <summary>
/// Startup Routine for <see cref="PersistenceSqliteAssembly"/>.
/// </summary>
public static class StartupService
{
    /// <summary>
    /// Registers and configures <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    public static void AddPersistenceSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        Configuration.Setup(configuration);

        services.AddSingleton<ValueConverter<Ulid, string>, UlidToStringConverter>();
        services.AddDbContext<InhouseCoreDbContext>(options =>
            options.UseSqlite(Configuration.Data.SqliteConnectionString, sqliteOptions =>
                sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
            services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddRepositories();
    }

    /// <summary>
    /// Registeres and configures Authentication and Authorization services.
    /// </summary>
    /// <param name="services"></param>
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<InhouseCoreDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<User, InhouseCoreDbContext>();

        services
            .AddAuthentication(options => 
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.Identity.Jwt.Issuer,
                    ValidAudience = Configuration.Identity.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Identity.Jwt.EncryptionKey)),
                };
            })
            .AddDiscord(options =>
            {
                options.ClientId = Configuration.Identity.Discord.ClientId;
                options.ClientSecret = Configuration.Identity.Discord.ClientSecret;

                options.AuthorizationEndpoint = Configuration.Identity.Discord.AuthorizationEndpoint;
                options.TokenEndpoint = Configuration.Identity.Discord.TokenEndpoint;
                options.UserInformationEndpoint = Configuration.Identity.Discord.UserInformationEndpoint;

                options.CallbackPath = new PathString(Configuration.Identity.Discord.OAuthCallback);
                options.AccessDeniedPath = new PathString("/access-denied");

                options.Scope.Add("identify");
                options.Scope.Add("email");

                #region Claims
                
                // Discord User object structure can be found at https://discord.com/developers/docs/resources/user#user-object
                
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.Id, "id");
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.Username, "username");
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.Discriminator, "discriminator");

                options.ClaimActions.MapJsonKey(DiscordClaimTypes.Email, "email?");
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.Verified, "verified?");

                options.ClaimActions.MapJsonKey(DiscordClaimTypes.AvatarUrl, "avatar");
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.BannerUrl, "banner?");
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.AccentColor, "accent_color?");
                options.ClaimActions.MapJsonKey(DiscordClaimTypes.Locale, "locale?");

                //options.ClaimActions.MapJsonKey(DiscordClaimTypes.IsBot, "bot?");
                //options.ClaimActions.MapJsonKey(DiscordClaimTypes.MfaEnabled, "mfa_enabled?");

                #endregion

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                        {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted); ;
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();
                        var user = JsonDocument.Parse(content).RootElement;
                        context.RunClaimActions(user);
                    }
                };
            })
            .AddIdentityServerJwt();
    }

    /// <summary>
    /// Executes all pending migrations on <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> .</param>
    public static void MigrateSqliteDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<InhouseCoreDbContext>();
        
        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();
    }

    /// <summary>
    /// Registers all repositories to DI.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> .</param>
    private static void AddRepositories(this IServiceCollection services)
    {
        var assemblyTypes = PersistenceSqliteAssembly.Value.GetTypes();
        var repositoryTypes = assemblyTypes
            .Where(x => x.Name.EndsWith("Repository"))
            .Where(x => !x.IsAbstract);

        foreach (var implementationType in repositoryTypes)
        {
            services.AddTransient(implementationType);
            foreach (var serviceType in implementationType.GetInterfaces())
                services.AddTransient(serviceType, implementationType);
        }
    }
}
