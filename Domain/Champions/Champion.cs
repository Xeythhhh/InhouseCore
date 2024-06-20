using Domain.Primitives;
using Domain.Primitives.Result;

using FluentValidation.Results;

namespace Domain.Champions;

/// <summary>Represents a champion entity.</summary>
public sealed partial class Champion :
    AggregateRoot<ChampionId>
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
            : Result.Failure<Champion>(
                Errors.Validation(validationResult.Errors));
    }
}
