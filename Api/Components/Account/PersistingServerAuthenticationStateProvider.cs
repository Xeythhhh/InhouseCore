using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using SharedKernel;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Infrastructure;

namespace Api.Components.Account;
// This is a server-side AuthenticationStateProvider that uses PersistentComponentState to flow the
// authentication state to the client which is then fixed for the lifetime of the WebAssembly application.
internal sealed class PersistingServerAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
{
    private readonly PersistentComponentState state;
    private readonly IdentityOptions options;

    private readonly PersistingComponentStateSubscription subscription;

    private Task<AuthenticationState>? authenticationStateTask;

    public PersistingServerAuthenticationStateProvider(
        PersistentComponentState persistentComponentState,
        IOptions<IdentityOptions> optionsAccessor)
    {
        state = persistentComponentState;
        options = optionsAccessor.Value;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> task) => authenticationStateTask = task;

    private async Task OnPersistingAsync() =>
        await Result.OkIf(authenticationStateTask is not null, new Error($"Authentication state not set in {nameof(OnPersistingAsync)}()."))
            .Bind(() => Result.Try(async () => await authenticationStateTask!))
            .Map(authenticationState => authenticationState.User)
            .Ensure(user => user.Identity?.IsAuthenticated ?? false, "Not authenticated.")
            .Bind(user => UserInfoDto.Create(
                user.GetClaimValueOrThrow(options.ClaimsIdentity.UserIdClaimType),
                user.GetClaimValueOrThrow(options.ClaimsIdentity.EmailClaimType),
                user.GetClaimValueOrThrow(AppConstants.Discord.Claims.Id),
                user.GetClaimValueOrThrow(AppConstants.Discord.Claims.Username),
                user.GetClaimValueOrThrow(AppConstants.Discord.Claims.Avatar),
                user.GetClaimValueOrThrow(AppConstants.Discord.Claims.Verified)
            ))
            .Tap(dto => state.PersistAsJson(nameof(UserInfoDto), dto));

    public void Dispose()
    {
        subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
    }
}
