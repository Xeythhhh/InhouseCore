using System.Text.Json;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using WebApp.Configuration;

namespace WebApp.Layout;
public partial class MainLayout
{
    [Inject] private IHttpClientFactory HttpClientFactory { get; set; }

    public static MudTheme Theme { get; set; }
    private bool _isDarkMode;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("Content");
            using HttpResponseMessage response = await httpClient.GetAsync("theme.json");
            response.EnsureSuccessStatusCode();

            await using Stream stream = await response.Content.ReadAsStreamAsync();

#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            JsonSerializerOptions options = new()
            {
                Converters = { new MudColorConverter() },
                PropertyNameCaseInsensitive = true
            };
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

            Theme = await JsonSerializer.DeserializeAsync<MudTheme>(stream, options)
                    ?? throw new JsonException("Invalid theme json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading theme: {ex.Message}");
        }
    }

    private bool _drawerOpen = true;
    private void DrawerToggle() => _drawerOpen = !_drawerOpen;
}
