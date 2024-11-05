using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

using Domain.Users;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

using MudBlazor;

namespace Api.Components.Account.Pages;
public partial class ExternalLogin
{
    public const string LoginCallbackAction = "LoginCallback";

    private string? message;
    private ExternalLoginInfo externalLoginInfo = default!;
    private string? ProviderDisplayName => externalLoginInfo.ProviderDisplayName;

    [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; }
    [Inject] private UserManager<ApplicationUser> UserManager { get; set; }
    [Inject] private IUserStore<ApplicationUser> UserStore { get; set; }
    [Inject] private IEmailSender<ApplicationUser> EmailSender { get; set; }
    [Inject] private ILogger<ExternalLogin> Logger { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IdentityRedirectManager RedirectManager { get; set; }

    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    [SupplyParameterFromForm] private ExternalLoginModel Model { get; set; } = new();
    [SupplyParameterFromQuery] private string? RemoteError { get; set; }
    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }
    [SupplyParameterFromQuery] private string? Action { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (RemoteError is not null)
        {
            RedirectManager.RedirectToWithStatus("Account/Login", $"Error from external provider: {RemoteError}", HttpContext);
        }

        ExternalLoginInfo? info = await SignInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            RedirectManager.RedirectToWithStatus("Account/Login", "Error loading external login information.", HttpContext);
        }

        externalLoginInfo = info;

        if (HttpMethods.IsGet(HttpContext.Request.Method))
        {
            if (Action == LoginCallbackAction)
            {
                await OnLoginCallbackAsync();
                return;
            }

            // We should only reach this page via the login callback, so redirect back to
            // the login page if we get here some other way.
            RedirectManager.RedirectTo("Account/Login");
        }
    }

    private async Task OnLoginCallbackAsync()
    {
        // Sign in the user with this external login provider if the user already has a login.
        SignInResult result = await SignInManager.ExternalLoginSignInAsync(
            externalLoginInfo.LoginProvider,
            externalLoginInfo.ProviderKey,
            isPersistent: false,
            bypassTwoFactor: true);

        if (result.Succeeded)
        {
            Logger.LogInformation(
                "{Name} logged in with {LoginProvider} provider.",
                externalLoginInfo.Principal.Identity?.Name,
                externalLoginInfo.LoginProvider);
            RedirectManager.RedirectTo(ReturnUrl);
        }
        else if (result.IsLockedOut)
        {
            RedirectManager.RedirectTo("Account/Lockout");
        }

        // If the user does not have an account, then ask the user to create an account.
        if (externalLoginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
        {
            Model.Email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email) ?? "";
        }
    }

    private async Task OnValidSubmitAsync()
    {
        IUserEmailStore<ApplicationUser> emailStore = GetEmailStore();
        ApplicationUser user = CreateUser();

        await UserStore.SetUserNameAsync(user, Model.Email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, Model.Email, CancellationToken.None);

        IdentityResult result = await UserManager.CreateAsync(user);
        if (result.Succeeded)
        {
            result = await UserManager.AddLoginAsync(user, externalLoginInfo);
            if (result.Succeeded)
            {
                if (externalLoginInfo.Principal.FindFirst("id") is { } idClaim)
                    await UserManager.AddClaimAsync(user, idClaim);

                if (externalLoginInfo.Principal.FindFirst("username") is { } displayNameClaim)
                    await UserManager.AddClaimAsync(user, displayNameClaim);

                Logger.LogInformation("User created an account using {Name} provider.", externalLoginInfo.LoginProvider);

                string userId = await UserManager.GetUserIdAsync(user);
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                string callbackUrl = NavigationManager.GetUriWithQueryParameters(
                    NavigationManager.ToAbsoluteUri("Account/ConfirmEmail").AbsoluteUri,
                    new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code });
                await EmailSender.SendConfirmationLinkAsync(user, Model.Email, HtmlEncoder.Default.Encode(callbackUrl));

                // If account confirmation is required, we need to show the link if we don't have a real email sender
                if (UserManager.Options.SignIn.RequireConfirmedAccount)
                {
                    RedirectManager.RedirectTo("Account/RegisterConfirmation", new() { ["email"] = Model.Email });
                }

                await SignInManager.SignInAsync(user, isPersistent: false, externalLoginInfo.LoginProvider);
                RedirectManager.RedirectTo(ReturnUrl);
            }
        }

        message = $"Error: {string.Join(",", result.Errors.Select(error => error.Description))}";
    }

    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor");
        }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore() => !UserManager.SupportsUserEmail
            ? throw new NotSupportedException("The default UI requires a user store with email support.")
            : (IUserEmailStore<ApplicationUser>)UserStore;

    private sealed class ExternalLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}