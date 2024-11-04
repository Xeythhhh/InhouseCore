using Domain.Primitives;

using SharedKernel.Primitives.Result;

using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

/// <summary>Represents an application role.</summary>
public sealed class ApplicationRole :
    IdentityRole<AspNetIdentityId>,
    IEntity<AspNetIdentityId>
{
    /// <summary>Gets or initializes the creation date of the role.</summary>
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the last updated date of the role.</summary>
    public DateTime LastUpdatedAtUtc { get; set; }

    /// <summary>Parameterless constructor required by EF Core, Identity services and auto-mappings.</summary>
    public ApplicationRole() { }

    /// <summary>Creates a new instance of <see cref="ApplicationRole"/> with the specified name.</summary>
    /// <param name="name">The name of the role.</param>
    /// <returns>A result containing the new <see cref="ApplicationRole"/> or an error.</returns>
    public static Result<ApplicationRole> Create(string name) =>
        Result.Try(() =>
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
                return name.Trim();
            })
            .Map(trimmedName => new ApplicationRole() { Name = trimmedName });
}
