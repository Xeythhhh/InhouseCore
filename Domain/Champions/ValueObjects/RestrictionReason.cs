using Domain.Errors;
using Domain.Primitives;

using SharedKernel.Primitives.Result;

namespace Domain.Champions.ValueObjects;

/// <summary>Value object for the Restriction's Reason.</summary>
public sealed record RestrictionReason : ValueObject<string>
{
    private RestrictionReason(string value) { Value = value; }

    /// <summary>Creates a new <see cref="RestrictionReason"/> instance.</summary>
    /// <param name="value">The reason to create.</param>
    /// <returns>A result containing the <see cref="RestrictionReason"/> if successful.</returns>
    public static Result<RestrictionReason> Create(string? value) =>
        Result.Try(() => value?.Trim())
            .Ensure(identifier => !string.IsNullOrEmpty(identifier), new DomainErrors.NullOrEmptyError())
            .Map(identifier => new RestrictionReason(identifier!));

    /// <summary>Gets the atomic values of the value object.</summary>
    /// <returns>An enumerable of atomic values.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>Implicit conversion from string to <see cref="RestrictionReason"/>.</summary>
    /// <param name="value">The string value to convert.</param>
    public static implicit operator RestrictionReason(string value)
    {
        Result<RestrictionReason> result = Create(value);
        return result.IsSuccess ? result.Value
            : throw DomainErrors.InvalidValueError.Exception();
    }
}
