using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using SharedKernel;

namespace WebApp.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetClaimValue(this ClaimsPrincipal principal, string claimType, string? fallbackValue = null) =>
        principal.FindFirst(claimType)?.Value
            ?? fallbackValue;

    public static string GetClaimValueOrThrow(this ClaimsPrincipal principal, string claimType, string? fallbackValue = null) =>
        principal.GetClaimValue(claimType, fallbackValue)
        ?? throw new ArgumentException($"No Claim with type '{claimType}' found.");
}
