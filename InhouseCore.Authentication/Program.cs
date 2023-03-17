using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.AddAuthentication();

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
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
