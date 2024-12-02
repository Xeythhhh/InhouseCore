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
            public static Result<AugmentTarget> Create(string? value, string? gameName = null) =>
                Result.Try(() =>
                {
                    string? trimmed = value?.Trim();
                    string? formatted = !string.IsNullOrEmpty(trimmed)
                        ? char.ToUpper(trimmed[0]) + trimmed[1..].ToLower()
                        : trimmed;

                    ArgumentException.ThrowIfNullOrWhiteSpace(formatted);

                    gameName ??= ValidValues.FirstOrDefault(h => h.Value.Contains(formatted)).Key;

                    ArgumentException.ThrowIfNullOrWhiteSpace(gameName);

                    return formatted!;
                }, exception => new ValueOutOfRangeError().CausedBy(exception))
            .Ensure(target =>
                ValidValues.TryGetValue(gameName!, out var targets) &&
                targets.Any(t => t.Equals(target, StringComparison.CurrentCultureIgnoreCase)),
                new ValueOutOfRangeError())
                .Map(Target => new AugmentTarget(Target));

            /// <summary>Gets the atomic values used for equality comparison.</summary>
            /// <returns>An enumerable of atomic values.</returns>
            protected override IEnumerable<object?> GetAtomicValues()
            {
                yield return Value;
            }

            static AugmentTarget()
            {
                AddValidValues("default", ["default-Q", "default-E", "default-R", "default-D", "default-F"]);
            }

            /// <summary>Gets the valid values for a Augment Target.</summary>
            private static readonly Dictionary<string, HashSet<string>> ValidValues = new();
            public static void AddValidValues(string? gameName, string[]? values)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(gameName);

                if (ValidValues.ContainsKey(gameName)) throw new InvalidOperationException("Game Augments are already registered");

                if (values is not null)
                {
                    List<string> formatted = values.Select(v =>
                    {
                        string[] strings = v.Split('-');
                        return strings[0] != gameName
                            ? throw new InvalidOperationException("Game Augments must have the gameName-* suffix")
                            : strings[1];
                    }).ToList();

                    ValidValues.Add(gameName, formatted.ToHashSet());
                }
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
