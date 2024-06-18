using CSharpFunctionalExtensions;

using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities;
/// <summary>Strongly-typed Id for <see cref="Champion"/></summary>
public sealed record ChampionId(long Value) : EntityId<Champion>(Value);
public sealed class Champion : EntityBase<ChampionId>
{
    public string Name { get; set; } = string.Empty;
    public Classes Class { get; init; }
    public Roles Role { get; init; }

    private Champion()
    {
        // Private constructor required by EF Core and auto-mappings
    }

    public static Result<Champion> Create(
        string name,
        Classes @class,
        Roles role)
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

    public enum Classes
    {
        Melee = 0,
        Ranged = 1
    }

    public enum Roles
    {
        Dps = 0,
        Support = 1
    }

    private class Validator : AbstractValidator<Champion>
    {
        public static Validator Instance = new();

        public Validator()
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