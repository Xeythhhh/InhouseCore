using System.Security.Claims;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp.Infrastructure;

public partial class Home
{
#pragma warning disable IDE1006 // Naming Styles
    [Inject] private AuthenticationStateProvider _authenticationStateProvider { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    private PersistentAuthenticationStateProvider AuthenticationStateProvider =>
        (PersistentAuthenticationStateProvider)_authenticationStateProvider;

    public ClaimsPrincipal? User { get; set; }
    public string? Username { get; set; }

    protected override async void OnInitialized()
    {
        AuthenticationState? authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        User = authState.User;

        Console.Write(User.Claims.Count());

        foreach (var claim in User?.Claims ?? Array.Empty<Claim>())
        {
            Console.WriteLine($"{claim.Value}|{claim.Type}|{claim.Issuer}");
        }

        // Extract username from claims
        Username = User?.FindFirst(ClaimTypes.Name)?.Value;
    }
}
