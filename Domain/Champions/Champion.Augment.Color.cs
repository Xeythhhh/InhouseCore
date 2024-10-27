using System.Drawing;
using System.Text.RegularExpressions;

using Domain.Primitives;

using SharedKernel.Extensions;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    public sealed partial class Augment
    {
        /// <summary>Value object for the augment target's hex color code.</summary>
        public sealed partial record AugmentColor : ValueObject<string>
        {
            private AugmentColor(string value) { Value = value; }

            /// <summary>Creates a <see cref="AugmentColor"/> if the value is valid. Returns a failure result if the color is invalid.</summary>
            /// <param name="value">The value of the augment's hex color code.</param>
            /// <returns>A <see cref="Result{AugmentColor}"/> indicating success or failure.</returns>
            public static Result<AugmentColor> Create(string? value) =>
                Result.Try(() =>
                {
                    string? trimmed = value?.Trim()?.ToLower();
                    ArgumentException.ThrowIfNullOrWhiteSpace(value);

                    return !IsValidHexOrColorName(trimmed!)
                        ? throw new ArgumentException("Invalid color name / hex format", nameof(value))
                        : trimmed!;
                })
                .Map(hexOrColorName => Color.FromName(hexOrColorName).IsKnownColor
                    ? Color.FromName(hexOrColorName)
                    : ColorTranslator.FromHtml(hexOrColorName))
                .Map(hex => new AugmentColor(hex.ColorToHex()));

            /// <summary>Gets the atomic values used for equality comparison.</summary>
            /// <returns>An enumerable of atomic values.</returns>
            protected override IEnumerable<object?> GetAtomicValues()
            {
                yield return Value;
            }

            /// <summary>Implicitly converts a <see cref="string"/> to a <see cref="AugmentColor"/>.</summary>
            /// <param name="value">The value to convert.</param>
            public static implicit operator AugmentColor(string value)
            {
                Result<AugmentColor> result = Create(value);
                return result.IsSuccess ? result.Value
                    : throw new InvalidOperationException(result.Errors[0].Message);
            }

            /// <summary>Explicitly converts a <see cref="AugmentColor"/> to a <see cref="string"/>.</summary>
            /// <param name="role">The <see cref="AugmentColor"/> to convert.</param>
            public static explicit operator string(AugmentColor role) => role.Value;

            [GeneratedRegex("^#(?:[0-9a-fA-F]{3,8})$")]
            private static partial Regex ValidColor();

            /// <summary>Checks if the provided value is a valid hex color code or a known color name.</summary>
            private static bool IsValidHexOrColorName(string value) => Color.FromName(value).IsKnownColor || ValidColor().Match(value).Success;
        }
    }
}
