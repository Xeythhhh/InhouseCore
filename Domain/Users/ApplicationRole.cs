using Domain.Primitives;

using FluentResults;

using FluentValidation.Results;
using SharedKernel.Extensions;

using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

/// <summary>Represents an application role.</summary>
public sealed partial class ApplicationRole :
    IdentityRole<AspNetIdentityId>,
    IEntity<AspNetIdentityId>
{
    /// <summary>Gets or initializes the creation date of the role.</summary>
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the last updated date of the role.</summary>
    public DateTime LastUpdatedAtUtc { get; set; }

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    private ApplicationRole() { }

    /// <summary>Creates a new instance of <see cref="ApplicationRole"/> with the specified name.</summary>
    /// <param name="name">The name of the role.</param>
    /// <returns>A result containing the new <see cref="ApplicationRole"/> or an error.</returns>
    public static Result<ApplicationRole> Create(string name)
    {
        name = name?.Trim() ?? string.Empty;
        ApplicationRole instance = new() { Name = name };
        ValidationResult validationResult = Validator.Instance.Validate(instance);
        return validationResult.IsValid
            ? Result.Ok(instance)
            : Result.Fail<ApplicationRole>(new Error("An error occurred validating new ApplicationRole")
                .WithMetadata(typeof(ApplicationRole).Name, instance)
                .CausedBy(validationResult));
    }
}
