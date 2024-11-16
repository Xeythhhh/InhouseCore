using System.Security.Claims;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

using WebApp.Extensions;

namespace WebApp.Infrastructure;

public partial class Home
{
    private ClaimsPrincipal? User { get; set; }

    [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    protected async override void OnInitialized()
    {
        await AuthenticationStateProvider.InitializeUser(user => User = user, Snackbar);
        base.OnInitialized();
    }
}
