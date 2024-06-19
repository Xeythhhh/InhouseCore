using Domain.Primitives;
using Domain.Primitives.Result;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

/// <summary>Represents an application role.</summary>
public sealed class ApplicationRole :
    IdentityRole<AspNetIdentityId>,
    IEntity<AspNetIdentityId>
{
    /// <summary>Gets or initializes the creation date of the role.</summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the last updated date of the role.</summary>
    public DateTime LastUpdatedAt { get; set; }

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
            ? Result.Success(instance)
            : Result.Failure<ApplicationRole>(
                new Error("Validation", string.Join(", ",
                    validationResult.Errors.Select(e => e.ErrorMessage))));
    }

    /// <summary>Validator for <see cref="ApplicationRole"/>.</summary>
    private class Validator : AbstractValidator<ApplicationRole>
    {
        /// <summary>Singleton instance of the validator.</summary>
        public static Validator Instance = new();

        public Validator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
