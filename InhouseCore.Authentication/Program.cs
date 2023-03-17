using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;

using InhouseCore.SharedKernel.Configuration;

using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.AddAuthentication(IdentityConfiguration.AuthenticationSchemes.Default);
builder.Services.AddAuthorization();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddBlazorise()
    .AddMaterialProviders()
    .AddMaterialIcons();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();



//services
//    .AddAuthentication(options =>
//    {
//        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    })
//    .AddCookie()
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = Configuration.Identity.Jwt.Issuer,
//            ValidAudience = Configuration.Identity.Jwt.Audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Identity.Jwt.EncryptionKey)),
//        };
//    })
//    .AddDiscord(options =>
//    {
//        options.ClientId = Configuration.Identity.Discord.ClientId;
//        options.ClientSecret = Configuration.Identity.Discord.ClientSecret;

//        options.AuthorizationEndpoint = Configuration.Identity.Discord.AuthorizationEndpoint;
//        options.TokenEndpoint = Configuration.Identity.Discord.TokenEndpoint;
//        options.UserInformationEndpoint = Configuration.Identity.Discord.UserInformationEndpoint;

//        options.CallbackPath = new PathString(Configuration.Identity.Discord.OAuthCallback);
//        options.AccessDeniedPath = new PathString("/access-denied");

//        options.Scope.Add("identify");
//        options.Scope.Add("email");

//        #region Claims

//        // Discord User object structure can be found at https://discord.com/developers/docs/resources/user#user-object

//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.Id, "id");
//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.Username, "username");
//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.Discriminator, "discriminator");

//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.Email, "email?");
//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.Verified, "verified?");

//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.AvatarUrl, "avatar");
//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.BannerUrl, "banner?");
//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.AccentColor, "accent_color?");
//        options.ClaimActions.MapJsonKey(DiscordClaimTypes.Locale, "locale?");

//        //options.ClaimActions.MapJsonKey(DiscordClaimTypes.IsBot, "bot?");
//        //options.ClaimActions.MapJsonKey(DiscordClaimTypes.MfaEnabled, "mfa_enabled?");

//        #endregion

//        options.Events = new OAuthEvents
//        {
//            OnCreatingTicket = async context =>
//            {
//                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
//                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

//                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted); ;
//                response.EnsureSuccessStatusCode();

//                var content = await response.Content.ReadAsStringAsync();
//                var user = JsonDocument.Parse(content).RootElement;
//                context.RunClaimActions(user);
//            }
//        };
//    })