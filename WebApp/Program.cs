using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using SharedKernel;

using WebApp;
using WebApp.Pages.Champions.Dialogs.Augment.Abstract;
using WebApp.Pages.Champions.Dialogs.Restrictions.Abstract;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.AddSharedSettings();

builder.Configuration.AddJsonFile("./appsettings.Development.json");
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiAddress")
                    ?? throw new InvalidOperationException("Invalid Configuration"))
});

builder.Services.AddScoped<AugmentModelBase.Validator>();
builder.Services.AddScoped<RestrictionModelBase.Validator>();

await builder.Build().RunAsync();
