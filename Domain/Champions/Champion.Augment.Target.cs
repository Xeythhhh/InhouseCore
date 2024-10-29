using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    public sealed partial class Augment
    {
        /// <summary>Value object for the AugmentTarget.</summary>
        public sealed record AugmentTarget : ValueObject<string>
        {
            private AugmentTarget(string value) { Value = value; }

            /// <summary>Creates a <see cref="AugmentTarget"/> if the value is valid.</summary>
            /// <param name="value">The value of the Augment's Target.</param>
            /// <returns>A <see cref="Result{AugmentTarget}"/> indicating success or failure.</returns>
            public static Result<AugmentTarget> Create(string? value) =>
                Result.Try(() =>
                {
                    string? trimmed = value?.Trim();
                    string? formatted = !string.IsNullOrEmpty(trimmed)
                        ? char.ToUpper(trimmed[0]) + trimmed[1..].ToLower()
                        : trimmed;

                    ArgumentException.ThrowIfNullOrWhiteSpace(formatted);
                    return formatted!;
                })
                .Ensure(ValidValues.Contains, new ValueOutOfRangeError())
                .Map(Target => new AugmentTarget(Target));

            /// <summary>Gets the atomic values used for equality comparison.</summary>
            /// <returns>An enumerable of atomic values.</returns>
            protected override IEnumerable<object?> GetAtomicValues()
            {
                yield return Value;
            }

            /// <summary>Gets the valid values for a Augment Target.</summary>
            private static HashSet<string> ValidValues = ["q", "e", "r", "d", "f"];
            internal static void ConfigureValidValues(string[]? values)
            {
                if (values is not null)
                    ValidValues = values.ToHashSet();
            }

            /// <summary>Implicitly converts a <see cref="string"/> to a <see cref="AugmentTarget"/>.</summary>
            /// <param name="value">The value to convert.</param>
            public static implicit operator AugmentTarget(string value)
            {
                Result<AugmentTarget> result = Create(value);
                return result.IsSuccess ? result.Value
                    : throw new InvalidOperationException(result.Errors[0].Message);
            }

            /// <summary>Explicitly converts a <see cref="AugmentTarget"/> to a <see cref="string"/>.</summary>
            /// <param name="role">The <see cref="AugmentTarget"/> to convert.</param>
            public static explicit operator string(AugmentTarget role) => role.Value;

            public sealed class ValueOutOfRangeError() : Error(MessageTemplate)
            {
                internal static string MessageTemplate = $"'Target' value outside of supported range. (valid values: '{string.Join(", ", ValidValues)}'";
            }
        }
    }
}
