using Domain.Errors;
using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions.ValueObjects;

/// <summary>Value object for the Champion's name.</summary>
public sealed record ChampionName : ValueObject<string>
{
    private ChampionName(string value) { Value = value; }

    /// <summary>Creates a new <see cref="ChampionName"/> instance.</summary>
    /// <param name="value">The name value to create.</param>
    /// <returns>A result containing the <see cref="ChampionName"/> if successful.</returns>
    public static Result<ChampionName> Create(string? value) =>
        Result.Try(() => value?.Trim())
            .Ensure(name => !string.IsNullOrEmpty(name), new DomainErrors.NullOrEmptyError())
            .Map(name => name!)
            .Ensure(name => name.Length < 101, new GreaterThan101CharactersError())
            .Map(name => new ChampionName(name));

    /// <summary>Gets the atomic values of the value object.</summary>
    /// <returns>An enumerable of atomic values.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>Implicit conversion from string to <see cref="ChampionName"/>.</summary>
    /// <param name="value">The string value to convert.</param>
    public static implicit operator ChampionName(string value)
    {
        Result<ChampionName> result = Create(value);
        return result.IsSuccess ? result.Value
            : throw DomainErrors.InvalidValueError.Exception();
    }

    /// <summary>Explicit conversion from <see cref="ChampionName"/> to string.</summary>
    /// <param name="name">The <see cref="ChampionName"/> to convert.</param>
    public static explicit operator string(ChampionName name) => name.Value;

    public class GreaterThan101CharactersError() : Error("Champion Name cannot exceed 101 characters.");
    public class NameIsNotUniqueError(string? name) : Error($"Champion Name is not unique. '{name}'");
}
