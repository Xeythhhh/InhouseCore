using System.Security.Claims;
using System.Text.Json;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

using SharedKernel;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Configuration;
using WebApp.Extensions;

namespace WebApp.Layout;
public partial class MainLayout
{
    public static MudTheme Theme { get; set; }
    private MudThemeProvider ThemeProvider { get; set; }

    [Inject] private IHttpClientFactory HttpClientFactory { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    public ClaimsPrincipal? User { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await AuthenticationStateProvider.InitializeUser(user => User = user, Snackbar);
        await InitializeTheme();
    }

    private Task<Result> InitializeTheme() =>
        GetThemeStreamAsync()
            .OnSuccessTry(async stream => Theme = await DeserializeThemeAsync(stream))
            .Tap(async stream => await stream.DisposeAsync())
            .TapError(Snackbar.NotifyErrors)
            .ToResultWithoutValueAsync();

    private async Task<Result<Stream>> GetThemeStreamAsync() =>
        OperatingSystem.IsBrowser()
            ? await GetThemeStreamFromStaticContent()
            : Result.Try(() => new FileStream("theme.json", FileMode.Open, FileAccess.Read))
                .Map(fileStream => fileStream as Stream);

    private async Task<Result<Stream>> GetThemeStreamFromStaticContent() =>
        await Result.Try(() => HttpClientFactory.CreateClient(AppConstants.HttpClients.Content)
                .GetAsync("theme.json"))
            .Ensure(response => response is not null,
                new Error("HTTP Response was null"))
            .Bind(async response =>
                Result.Ok(await response.Content.ReadAsStreamAsync()));

#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
    private static async Task<MudTheme> DeserializeThemeAsync(Stream stream) =>
        await JsonSerializer.DeserializeAsync<MudTheme>(stream, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new MudColorConverter() }
        }) ?? throw new JsonException("Invalid theme JSON");
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

    private bool _isDarkMode;
    protected override async Task OnAfterRenderAsync(bool firstRender) =>
        await Result.OkIf(firstRender, "Not first render")
            .Tap(async () =>
            {
                _isDarkMode = await ThemeProvider.GetSystemPreference();
                StateHasChanged();
            });

    private bool _drawerOpen;
    void DrawerToggle() => _drawerOpen = !_drawerOpen;
}
