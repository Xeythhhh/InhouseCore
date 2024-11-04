using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace WebApp.Layout;
public partial class NavMenu
{
    private string? currentUrl;

    protected override void OnInitialized()
    {
        currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public void Dispose() => NavigationManager.LocationChanged -= OnLocationChanged;
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
}