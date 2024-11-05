using System.Text.Json;

using AspNet.Security.OAuth.Discord;

using Microsoft.AspNetCore.Authentication;

namespace Api.Extensions;

/// <summary> Provides extension methods for <see cref="DiscordAuthenticationOptions"/> to simplify claim mapping and scope management.</summary>
public static class DiscordAuthenticationOptionsExtensions
{
    /// <summary> Adds a custom JSON mapping for a claim type, allowing a custom resolver function to extract
    /// claim values from a JSON element.</summary>
    /// <param name="options">The <see cref="DiscordAuthenticationOptions"/> to which the mapping will be added.</param>
    /// <param name="claimType">The type of the claim to be mapped.</param>
    /// <param name="resolver">A function that extracts the claim value from a <see cref="JsonElement"/>.</param>
    /// <returns>The updated <see cref="DiscordAuthenticationOptions"/> for chained invocation.</returns>
    /// <remarks> Run the given <paramref name="resolver"/> to select a value from the json user data to add as a claim
    /// This no-ops if the returned value is empty</remarks>
    /// <exception cref="ArgumentNullException"> Thrown if <paramref name="options"/>, <paramref name="claimType"/>, or <paramref name="resolver"/> is <c>null</c>.</exception>
    public static DiscordAuthenticationOptions MapClaim(this DiscordAuthenticationOptions options, string claimType, Func<JsonElement, string?> resolver)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(claimType);
        ArgumentNullException.ThrowIfNull(resolver);

        options.ClaimActions.MapCustomJson(claimType, resolver);
        return options;
    }

    /// <summary> Maps multiple claim types to custom JSON mappings, allowing each claim type to use its own resolver function.</summary>
    /// <param name="options">The <see cref="DiscordAuthenticationOptions"/> to which the mappings will be added.</param>
    /// <param name="claimMappings">A collection of claim mappings, where each entry consists of a claim type and a resolver function.</param>
    /// <returns>The updated <see cref="DiscordAuthenticationOptions"/> for chained invocation.</returns>
    /// <remarks> Executes each resolver to extract claim values from JSON user data. If a resolver returns an empty value, no claim is added for that entry.</remarks>
    /// <exception cref="ArgumentNullException"> Thrown if <paramref name="options"/> or <paramref name="claimMappings"/> is <c>null</c>.</exception>
    public static DiscordAuthenticationOptions MapClaims(this DiscordAuthenticationOptions options, params (string claimType, Func<JsonElement, string?> resolver)[] claimMappings)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(claimMappings);

        foreach ((string claimType, Func<JsonElement, string?> resolver) in claimMappings)
        {
            ArgumentNullException.ThrowIfNull(claimType);
            ArgumentNullException.ThrowIfNull(resolver);

            options.MapClaim(claimType, resolver);
        }

        return options;
    }

    /// <summary> Adds one or more scopes to the <see cref="DiscordAuthenticationOptions"/>, specifying permissions for the OAuth authorization.</summary>
    /// <param name="options">The <see cref="DiscordAuthenticationOptions"/> to which the scopes will be added.</param>
    /// <param name="values">One or more scope values to add.</param>
    /// <returns>The updated <see cref="DiscordAuthenticationOptions"/> for chained invocation.</returns>
    /// <exception cref="ArgumentNullException"> Thrown if <paramref name="options"/> or <paramref name="values"/> is <c>null</c>.</exception>
    public static DiscordAuthenticationOptions AddScopes(this DiscordAuthenticationOptions options, params string[] values)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(values);

        options.Scope.Add(values);
        return options;
    }

    /// <summary> Adds one or more values to the collection of scopes, ensuring non-duplicate additions.</summary>
    /// <param name="scopes">The collection of scopes to which values will be added.</param>
    /// <param name="values">One or more scope values to add.</param>
    /// <returns>The updated collection of scopes with the added values.</returns>
    /// <remarks> This method prevents duplicate scope entries, so each value is only added once.</remarks>
    /// <exception cref="ArgumentNullException"> Thrown if <paramref name="scopes"/> or <paramref name="values"/> is <c>null</c>.</exception>
    public static ICollection<string> Add(this ICollection<string> scopes, params string[] values)
    {
        ArgumentNullException.ThrowIfNull(scopes);
        ArgumentNullException.ThrowIfNull(values);

        foreach (string scope in values)
        {
            if (!scopes.Contains(scope))
            {
                scopes.Add(scope);
            }
        }

        return scopes;
    }
}
