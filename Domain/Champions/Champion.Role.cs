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
        public static Result<ChampionRole> Create(string? value, string? game = null) =>
            Result.Try(() =>
            {
                string? trimmed = value?.Trim();
                string? formatted = !string.IsNullOrEmpty(trimmed)
                    ? char.ToUpper(trimmed[0]) + trimmed[1..].ToLower()
                    : trimmed;

                ArgumentException.ThrowIfNullOrWhiteSpace(formatted);

                game ??= ValidValues.FirstOrDefault(h => h.Value.Contains(formatted)).Key;

                ArgumentException.ThrowIfNullOrWhiteSpace(game);

                return formatted!;
            }, exception => new ValueOutOfRangeError().CausedBy(exception))
            .Ensure(role =>
                ValidValues.TryGetValue(game!, out var roles) &&
                roles.Any(r => r.Equals(role, StringComparison.CurrentCultureIgnoreCase)),
                new ValueOutOfRangeError())
            .Map(role => new ChampionRole(role));

        /// <summary>Gets the atomic values used for equality comparison.</summary>
        /// <returns>An enumerable of atomic values.</returns>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Value;
        }

        static ChampionRole()
        {
            AddValidValues("default", ["default-Tank", "default-Dps", "default-Healer"]);
        }

        /// <summary>Gets the valid values for a champion role.</summary>
        private static readonly Dictionary<string, HashSet<string>> ValidValues = new();

        public static void AddValidValues(string? game, string[]? values)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(game);

            if (ValidValues.ContainsKey(game)) throw new InvalidOperationException("Game Roles are already registered");

            if (values is not null)
            {
                List<string> formatted = values.Select(v =>
                {
                    string[] strings = v.Split('-');
                    return strings[0] != game
                        ? throw new InvalidOperationException("Game Roles must have the gameName-* suffix")
                        : strings[1];
                }).ToList();

                ValidValues.Add(game, formatted.ToHashSet());
            }
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
