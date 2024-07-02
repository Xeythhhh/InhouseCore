using Domain.Errors;
using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions.ValueObjects;

/// <summary>Value object for the Champion's role.</summary>
public sealed record ChampionRole : ValueObject<string>
{
    private ChampionRole(string value) { Value = value; }

    /// <summary>Creates a <see cref="ChampionRole"/> if the value is valid.</summary>
    /// <param name="value">The value of the champion role.</param>
    /// <returns>A <see cref="Result{ChampionRole}"/> indicating success or failure.</returns>
    public static Result<ChampionRole> Create(string? value) =>
        Result.Try(() => value?.Trim()?.ToLower())
            .OnSuccessTry(roleOrEmpty => ArgumentException.ThrowIfNullOrWhiteSpace(roleOrEmpty))
            .Map(role => role!)
            .Ensure(ValidValues.Contains, new ValueOutOfRangeError())
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
            : throw DomainErrors.InvalidValueForImplicitConversionError.Exception(result);
    }

    /// <summary>Explicitly converts a <see cref="ChampionRole"/> to a <see cref="string"/>.</summary>
    /// <param name="role">The <see cref="ChampionRole"/> to convert.</param>
    public static explicit operator string(ChampionRole role) => role.Value;

    public sealed class ValueOutOfRangeError() : Error($"Value outside of supported range. (valid values: '{string.Join(", ", ValidValues)}'");
}
