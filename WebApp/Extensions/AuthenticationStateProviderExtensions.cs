using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

using SharedKernel.Primitives.Result;
using SharedKernel.Extensions.ResultExtensions;

using System.Security.Claims;

namespace WebApp.Extensions;

public static class AuthenticationStateProviderExtensions
{
    /// <summary> Initializes the user's authentication state and sets the provided user reference with the current user.</summary>
    /// <param name="authenticationStateProvider">The <see cref="AuthenticationStateProvider"/> used to retrieve the authentication state.</param>
    /// <param name="setUserAction">An action that sets the <see cref="ClaimsPrincipal"/> representing the authenticated user.</param>
    /// <param name="snackbar">An instance of <see cref="ISnackbar"/> used to display any error messages that occur during initialization.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a <see cref="Result{T}"/> indicating success or failure.
    /// If successful, contains the <see cref="AuthenticationState"/> with the current user information.</returns>
    public static Task<Result<AuthenticationState>> InitializeUser(
        this AuthenticationStateProvider authenticationStateProvider,
        Action<ClaimsPrincipal?> setUserAction,
        ISnackbar snackbar) =>
        Result.Try(authenticationStateProvider.GetAuthenticationStateAsync)
            .Tap(authenticationState => setUserAction(authenticationState.User))
            .TapError(snackbar.NotifyErrors);
}
