using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    public sealed partial class Restriction
    {
        /// <summary>Value object for the RestrictionIdentifier.</summary>
        public sealed record RestrictionIdentifier : ValueObject<string>
        {
            private RestrictionIdentifier(string value) { Value = value; }

            /// <summary>Creates a <see cref="RestrictionIdentifier"/> if the value is valid.</summary>
            /// <param name="value">The value of the restriction's identifier.</param>
            /// <returns>A <see cref="Result{RestrictionIdentifier}"/> indicating success or failure.</returns>
            public static Result<RestrictionIdentifier> Create(string? value) =>
                Result.Try(() =>
                {
                    string? trimmed = value?.Trim()?.ToLower();
                    ArgumentException.ThrowIfNullOrWhiteSpace(value);
                    return trimmed!;
                })
                .Ensure(ValidValues.Contains, new ValueOutOfRangeError())
                .Map(identifier => new RestrictionIdentifier(identifier));

            /// <summary>Gets the atomic values used for equality comparison.</summary>
            /// <returns>An enumerable of atomic values.</returns>
            protected override IEnumerable<object?> GetAtomicValues()
            {
                yield return Value;
            }

            /// <summary>Gets the valid values for a restriction identifier.</summary>
            private static HashSet<string> ValidValues = ["q", "e", "r", "d", "f"];
            internal static void Initialize(string[]? values)
            {
                if (values is not null)
                    ValidValues = values.ToHashSet();
            }

            /// <summary>Implicitly converts a <see cref="string"/> to a <see cref="RestrictionIdentifier"/>.</summary>
            /// <param name="value">The value to convert.</param>
            public static implicit operator RestrictionIdentifier(string value)
            {
                Result<RestrictionIdentifier> result = Create(value);
                return result.IsSuccess ? result.Value
                    : throw new InvalidOperationException(result.Errors[0].Message);
            }

            /// <summary>Explicitly converts a <see cref="RestrictionIdentifier"/> to a <see cref="string"/>.</summary>
            /// <param name="role">The <see cref="RestrictionIdentifier"/> to convert.</param>
            public static explicit operator string(RestrictionIdentifier role) => role.Value;

            public sealed class ValueOutOfRangeError() : Error(MessageTemplate)
            {
                internal static string MessageTemplate = $"'Identifier' value outside of supported range. (valid values: '{string.Join(", ", ValidValues)}'";
            }
        }
    }
}
