using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Value object for the Champion's avatar URIs.</summary>
    public sealed record ChampionAvatar : ValueObject<string>
    {
        private ChampionAvatar(string portrait, string wide)
        {
            Portrait = portrait;
            PortraitWide = wide;
            Value = $"{portrait};{wide}";
        }

        /// <summary>Gets the URI of the champion's portrait image.</summary>
        public string Portrait { get; }

        /// <summary>Gets the URI of the champion's wide image.</summary>
        public string PortraitWide { get; }

        /// <summary>Returns the Portrait URI by default.</summary>
        public override string ToString() => Portrait;

        public static Result<ChampionAvatar> Create(string portraitAndWide)
        {
            string[] values = portraitAndWide.Split(";");
            return Create(values[0], values[1]);
        }

        /// <summary>Creates a new <see cref="ChampionAvatar"/> instance if the provided URIs are valid.</summary>
        /// <param name="portrait">The URI for the champion's portrait.</param>
        /// <param name="portraitWide">The URI for the champion's wide image.</param>
        /// <returns>A <see cref="Result{ChampionAvatar}"/> containing the created avatar if successful, otherwise an error result.</returns>
        public static Result<ChampionAvatar> Create(string? portrait, string? portraitWide) =>
            Result.Try(() =>
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(portrait, nameof(portrait));
                ArgumentException.ThrowIfNullOrWhiteSpace(portraitWide, nameof(portraitWide));

                Uri.TryCreate(portrait, UriKind.Absolute, out var portraitUri);
                Uri.TryCreate(portraitWide, UriKind.Absolute, out var wideUri);

                if (portraitUri?.IsAbsoluteUri != true)
                    throw new UriFormatException($"Invalid URI format for {nameof(portrait)}.");

                if (wideUri?.IsAbsoluteUri != true)
                    throw new UriFormatException($"Invalid URI format for {nameof(portraitWide)}.");

                return new ChampionAvatar(portrait, portraitWide);
            }, exception => new InvalidUriError().CausedBy(exception));

        /// <summary>Gets the atomic values of the value object.</summary>
        /// <returns>An enumerable of atomic values.</returns>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Value;
            yield return Portrait;
            yield return PortraitWide;
        }

        /// <summary>Implicit conversion from string to <see cref="ChampionAvatar"/>.</summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator ChampionAvatar((string Portrait, string Wide) value)
        {
            Result<ChampionAvatar> result = Create(value.Portrait, value.Wide);
            return result.IsSuccess ? result.Value
                : throw new InvalidOperationException(result.Errors[0].Message);
        }

        public class InvalidUriError() : Error($"The URI is invalid.");
    }
}
