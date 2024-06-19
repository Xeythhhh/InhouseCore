using CSharpFunctionalExtensions;

using Domain.Primitives;

using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities;

/// <summary>Strongly-typed Id for <see cref="Champion"/>.</summary>
public sealed record ChampionId(long Value)
    : EntityId<Champion>(Value);

/// <summary>Represents a champion entity.</summary>
public sealed class Champion
    : EntityBase<ChampionId>
{
    /// <summary>Gets or sets the name of the champion.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Gets the class of the champion.</summary>
    public Classes Class { get; init; }
    /// <summary>Gets the role of the champion.</summary>
    public Roles Role { get; init; }

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    private Champion() { }

    /// <summary>Creates a new instance of the <see cref="Champion"/> class.</summary>
    /// <param name="name">The name of the champion.</param>
    /// <param name="class">The class of the champion.</param>
    /// <param name="role">The role of the champion.</param>
    /// <returns>A result containing the created <see cref="Champion"/> instance if successful, otherwise a failure result.</returns>
    public static Result<Champion> Create(string name, Classes @class, Roles role)
    {
        Champion instance = new()
        {
            Name = name,
            Role = role,
            Class = @class
        };

        ValidationResult validationResult = Validator.Instance.Validate(instance);
        return validationResult.IsValid
            ? Result.Success(instance)
            : Result.Failure<Champion>(string.Join(", ",
                validationResult.Errors.Select(e => e.ErrorMessage)));
    }

    /// <summary>Enumeration of possible champion classes.</summary>
    public enum Classes
    {
        Melee = 0,
        Ranged = 1
    }

    /// <summary>Enumeration of possible champion roles.</summary>
    public enum Roles
    {
        Dps = 0,
        Support = 1
    }

    /// <summary>Validator for <see cref="Champion"/> instances.</summary>
    private sealed class Validator
        : AbstractValidator<Champion>
    {
        /// <summary>Gets a singleton instance of the <see cref="Validator"/> class.</summary>
        public static Validator Instance { get; } = new();

        /// <summary>Initializes a new instance of the <see cref="Validator"/> class.</summary>
        private Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("'Name' must not be empty.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            RuleFor(x => x.Class)
                .IsInEnum().WithMessage("Class must be a valid enum value.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Role must be a valid enum value.");
        }
    }
}
