using Domain.Errors;
using Domain.Primitives;

using SharedKernel.Primitives.Result;

namespace Domain.Champions.ValueObjects;

public sealed partial record RestrictionTarget
{
    /// <summary>Value object for the Restriction's Target Name.</summary>
    public sealed record TargetName : ValueObject<string>
    {
        private TargetName(string value) { Value = value; }

        /// <summary>Creates a new <see cref="TargetName"/> instance.</summary>
        /// <param name="value">The target name to create.</param>
        /// <returns>A result containing the <see cref="TargetName"/> if successful.</returns>
        public static Result<TargetName> Create(string? value) =>
            Result.Try(() => value?.Trim())
                .Ensure(name => !string.IsNullOrEmpty(name), new DomainErrors.NullOrEmptyError())
                .Map(name => new TargetName(name!));

        /// <summary>Gets the atomic values of the value object.</summary>
        /// <returns>An enumerable of atomic values.</returns>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Value;
        }

        /// <summary>Implicit conversion from string to <see cref="TargetName"/>.</summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator TargetName(string value)
        {
            Result<TargetName> result = Create(value);
            return result.IsSuccess ? result.Value
                : throw DomainErrors.InvalidValueError.Exception();
        }
    }
}
