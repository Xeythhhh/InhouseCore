using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Value object for the Champion's role.</summary>
    public sealed record ChampionRole : ValueObject<string>
    {
        private ChampionRole(string value) { Value = value; }

        /// <summary>Creates a <see cref="ChampionRole"/> if the value is valid.</summary>
        /// <param name="value">The value of the champion role.</param>
        /// <returns>A <see cref="Result{ChampionRole}"/> containing the created role if successful, otherwise an error result.</returns>
        public static Result<ChampionRole> Create(string? value) =>
            Result.Try(() =>
            {
                string? trimmed = value?.Trim()?.ToLower();
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                return trimmed!;
            })
            .Ensure(ValidValues.Contains, new ValueOutOfRangeError())
            .Map(role => new ChampionRole(role));

        /// <summary>Gets the atomic values used for equality comparison.</summary>
        /// <returns>An enumerable of atomic values.</returns>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Value;
        }


        /// <summary>Gets the valid values for a champion role.</summary>
        private static HashSet<string> ValidValues = ["tank", "dps", "healer"];
        internal static void ConfigureValidValues(string[]? values)
        {
            if (values is not null)
                ValidValues = values.ToHashSet();
        }

        /// <summary>Implicitly converts a <see cref="string"/> to a <see cref="ChampionRole"/>.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator ChampionRole(string value)
        {
            Result<ChampionRole> result = Create(value);
            return result.IsSuccess ? result.Value
                : throw new InvalidOperationException(result.Errors[0].Message);
        }

        /// <summary>Explicitly converts a <see cref="ChampionRole"/> to a <see cref="string"/>.</summary>
        /// <param name="role">The <see cref="ChampionRole"/> to convert.</param>
        public static explicit operator string(ChampionRole role) => role.Value;

        public sealed class ValueOutOfRangeError() : Error(MessageTemplate)
        {
            internal static string MessageTemplate = $"'Role' value outside of supported range. (valid values: '{string.Join(", ", ValidValues)}'";
        };
    }
}
