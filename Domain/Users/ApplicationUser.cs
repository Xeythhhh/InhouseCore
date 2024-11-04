using Domain.Primitives;

using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

/// <summary>Represents an application user with a strongly-typed identifier and domain event handling capabilities.</summary>
public sealed class ApplicationUser :
    IdentityUser<AspNetIdentityId>,
    IEntity<AspNetIdentityId>
{
    /// <summary>Gets the timestamp of when the user was created.</summary>
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the timestamp of when the user was last updated.</summary>
    public DateTime LastUpdatedAtUtc { get; set; }

    /// <summary>Parameterless constructor required by EF Core, Identity services and auto-mappings.</summary>
    public ApplicationUser() { }
}
