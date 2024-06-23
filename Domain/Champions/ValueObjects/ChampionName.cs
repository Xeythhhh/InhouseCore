using Domain.Primitives;

using FluentResults;

using SharedKernel.Extensions.ResultExtensions;

namespace Domain.Champions.ValueObjects;

/// <summary>Value object for the Champion's name.</summary>
public sealed record ChampionName : ValueObject<string>
{
    private ChampionName(string value) { Value = value; }

    /// <summary>Creates a new <see cref="ChampionName"/> instance.</summary>
    /// <param name="value">The name value to create.</param>
    /// <returns>A result containing the <see cref="ChampionName"/> if successful.</returns>
    public static Result<ChampionName> Create(string? value) =>
        Result.Ok(value?.Trim())
            .Ensure(name => !string.IsNullOrEmpty(name), ValueObjectCommonErrors.NullOrEmpty).Map(name => name!)
            .Ensure(name => name.Length < 101, Errors.GreaterThan101Characters)
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
            : throw new InvalidOperationException(ValueObjectCommonErrors.InvalidValueForImplicitConversion);
    }

    /// <summary>Explicit conversion from <see cref="ChampionName"/> to string.</summary>
    /// <param name="name">The <see cref="ChampionName"/> to convert.</param>
    public static explicit operator string(ChampionName name) => name.Value;

    /// <summary>Provides error messages for <see cref="ChampionName"/>.</summary>
    public static class Errors
    {
        /// <summary>Error message for name exceeding 101 characters.</summary>
        public static string GreaterThan101Characters => "Champion Name cannot exceed 101 characters";
        /// <summary>Error message for name in use.</summary>
        public static string NameIsNotUnique(string? name) => $"Champion Name is not unique. '{name}'";
    }
}
