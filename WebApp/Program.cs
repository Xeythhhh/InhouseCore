using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using SharedKernel;

using WebApp.Champions.Augments.Abstract;
using WebApp.Champions.Restrictions.Abstract;
using WebApp.Configuration;
using WebApp.Infrastructure;
using WebApp.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Configuration.AddJsonFile("./appsettings.Development.json", false, true);
await builder.AddSharedSettings();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiAddress")
                    ?? throw new InvalidOperationException("Invalid Configuration"))
});

builder.Services.Configure<ThemeConfiguration>(builder.Configuration.GetSection("ThemeConfiguration"));

builder.Services.AddScoped<IChampionService, ChampionService>();
builder.Services.AddScoped<AugmentModelBase.Validator>();
builder.Services.AddScoped<RestrictionModelBase.Validator>();

await builder.Build().RunAsync();
