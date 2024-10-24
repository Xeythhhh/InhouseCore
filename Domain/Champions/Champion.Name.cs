using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Value object for the Champion's name.</summary>
    public sealed record ChampionName : ValueObject<string>
    {
        private ChampionName(string value) { Value = value; }

        /// <summary>Creates a new <see cref="ChampionName"/> instance if the provided name is valid.</summary>
        /// <param name="value">The name of the champion. Must not be null or empty.</param>
        /// <returns>A <see cref="Result{ChampionName}"/> containing the created name if successful, otherwise an error result.</returns>
        public static Result<ChampionName> Create(string? value) =>
            Result.Try(() =>
            {
                string? trimmed = value?.Trim();
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                return trimmed!;
            })
            .Ensure(name => name.Length <= 100, new GreaterThan100CharactersError().CausedBy(new ArgumentException(null, nameof(value))))
            .Map(name => new ChampionName(name));

        /// <summary>Gets the atomic values of the value object.</summary>
        /// <returns>An enumerable of atomic values.</returns>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Value;
        }

        /// <summary>Implicit conversion from string to <see cref="ChampionName"/>.</summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator ChampionName(string value)
        {
            Result<ChampionName> result = Create(value);
            return result.IsSuccess ? result.Value
                : throw new InvalidOperationException(result.Errors[0].Message);
        }

        /// <summary>Explicit conversion from <see cref="ChampionName"/> to string.</summary>
        /// <param name="name">The <see cref="ChampionName"/> to convert.</param>
        public static explicit operator string(ChampionName name) => name.Value;

        public class GreaterThan100CharactersError() : Error("Champion Name cannot exceed 100 characters.");
        public class IsNotUniqueError(string? name) : Error($"Champion Name is not unique. '{name}'");
    }
}
