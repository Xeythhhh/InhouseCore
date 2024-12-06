using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using SharedKernel;

using WebApp.Champions.Augments.Abstract;
using WebApp.Champions.Restrictions.Abstract;
using WebApp.Infrastructure;
using WebApp.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Configuration.AddJsonFile("./appsettings.Development.json", false, true);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

//builder.Services.AddScoped(_ => new HttpClient
//{
//    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiAddress")
//                    ?? throw new InvalidOperationException("Invalid Configuration"))
//});

//builder.Services.AddHttpClient<MainLayout>(client =>
//    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient(AppConstants.HttpClients.Api, client =>
    client.BaseAddress = new Uri(builder.Configuration[AppConstants.HttpClients.ApiAddress]!));
//.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient(AppConstants.HttpClients.Content,
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient(AppConstants.HttpClients.Api));

builder.Services.AddScoped<ChampionService>();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<AugmentModelBase.Validator>();
builder.Services.AddScoped<RestrictionModelBase.Validator>();

await builder.Build().RunAsync();
