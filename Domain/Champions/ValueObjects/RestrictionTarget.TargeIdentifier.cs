using Domain.Errors;
using Domain.Primitives;

using SharedKernel.Primitives.Result;

namespace Domain.Champions.ValueObjects;

public sealed partial record RestrictionTarget
{
    /// <summary>Value object for the Restriction's Target Identifier.</summary>
    public sealed record TargetIdentifier : ValueObject<string>
    {
        private TargetIdentifier(string value) { Value = value; }

        /// <summary>Creates a new <see cref="TargetIdentifier"/> instance.</summary>
        /// <param name="value">The target identifier to create.</param>
        /// <returns>A result containing the <see cref="TargetIdentifier"/> if successful.</returns>
        public static Result<TargetIdentifier> Create(string? value) =>
            Result.Try(() => value?.Trim())
                .Ensure(identifier => !string.IsNullOrEmpty(identifier), new DomainErrors.NullOrEmptyError())
                .Map(identifier => new TargetIdentifier(identifier!));

        /// <summary>Gets the atomic values of the value object.</summary>
        /// <returns>An enumerable of atomic values.</returns>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Value;
        }

        /// <summary>Implicit conversion from string to <see cref="TargetIdentifier"/>.</summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator TargetIdentifier(string value)
        {
            Result<TargetIdentifier> result = Create(value);
            return result.IsSuccess ? result.Value
                : throw DomainErrors.InvalidValueError.Exception();
        }
    }
}
