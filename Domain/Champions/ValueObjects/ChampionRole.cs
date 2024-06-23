using Domain.Primitives;

using FluentResults;

using SharedKernel.Extensions.ResultExtensions;

namespace Domain.Champions.ValueObjects;

/// <summary>Value object for the Champion's role.</summary>
public sealed record ChampionRole : ValueObject<string>
{
    private ChampionRole(string value) { Value = value; }

    /// <summary>Creates a <see cref="ChampionRole"/> if the value is valid.</summary>
    /// <param name="value">The value of the champion role.</param>
    /// <returns>A <see cref="Result{ChampionRole}"/> indicating success or failure.</returns>
    public static Result<ChampionRole> Create(string? value) =>
        Result.Ok(value?.Trim()?.ToLower())
            .Ensure(role => !string.IsNullOrEmpty(role), ValueObjectCommonErrors.NullOrEmpty).Map(role => role!)
            .Ensure(ValidValues.Contains, Errors.ValueOutOfRange)
            .Map(role => new ChampionRole(role));

    /// <summary>Gets the atomic values used for equality comparison.</summary>
    /// <returns>An enumerable of atomic values.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>Gets the valid values for a champion role.</summary>
    private static HashSet<string> ValidValues => ["melee", "ranged", "support"];

    /// <summary>Implicitly converts a <see cref="string"/> to a <see cref="ChampionRole"/>.</summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator ChampionRole(string value)
    {
        Result<ChampionRole> result = Create(value);
        return result.IsSuccess ? result.Value
            : throw new InvalidOperationException(ValueObjectCommonErrors.InvalidValueForImplicitConversion);
    }

    /// <summary>Explicitly converts a <see cref="ChampionRole"/> to a <see cref="string"/>.</summary>
    /// <param name="role">The <see cref="ChampionRole"/> to convert.</param>
    public static explicit operator string(ChampionRole role) => role.Value;

    /// <summary>Provides error messages for <see cref="ChampionRole"/>.</summary>
    public static class Errors
    {
        /// <summary>Error message for invalid values.</summary>
        public static string ValueOutOfRange => $"Champion Role can only be '{string.Join(", ", ValidValues)}'";
    }
}
