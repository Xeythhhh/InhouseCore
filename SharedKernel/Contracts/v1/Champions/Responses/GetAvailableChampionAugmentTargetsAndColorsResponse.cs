using System.Collections.ObjectModel;

namespace SharedKernel.Contracts.v1.Champions.Responses;

/// <summary>Represents the response containing available augment targets and colors for a champion.</summary>
public sealed record GetAvailableChampionAugmentTargetsAndColorsResponse
{
    private const string ColorDelimiter = "|";

    /// <summary>Gets the collection of augment targets available for the champion.</summary>
    public IReadOnlyCollection<string> AugmentTargets { get; }

    /// <summary>Gets the dictionary mapping color labels to their corresponding hex codes.</summary>
    public IReadOnlyDictionary<string, string> AugmentColors { get; }

    /// <summary>Initializes a new instance of <see cref="GetAvailableChampionAugmentTargetsAndColorsResponse"/>.</summary>
    /// <param name="augmentTargets">A collection of augment targets available for the champion.</param>
    /// <param name="augmentColorLabelsAndColors">
    /// A collection of color strings in the format "label|hexcode", where <c>label</c> is a descriptive name
    /// and <c>hexcode</c> is the hexadecimal color code (e.g., "#FF5733").
    /// </param>
    public GetAvailableChampionAugmentTargetsAndColorsResponse(
        IEnumerable<string> augmentTargets,
        IEnumerable<string> augmentColorLabelsAndColors)
    {
        ArgumentNullException.ThrowIfNull(augmentTargets);
        ArgumentNullException.ThrowIfNull(augmentColorLabelsAndColors);

        AugmentTargets = augmentTargets.ToList().AsReadOnly();

        Dictionary<string, string> colorDictionary = new(StringComparer.OrdinalIgnoreCase);

        foreach (string value in augmentColorLabelsAndColors)
        {
            ValidateColorStringFormat(value);
            string[] parts = value.Split(ColorDelimiter);
            colorDictionary[parts[0]] = parts[1];
        }

        AugmentColors = new ReadOnlyDictionary<string, string>(colorDictionary);
    }

    /// <summary>Validates the format of a color string.</summary>
    /// <param name="value">The color string to validate.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the color string is null, empty, or not in the format "label|hexcode".
    /// </exception>
    private static void ValidateColorStringFormat(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        string[] parts = value.Split(ColorDelimiter);
        if (parts.Length != 2)
            throw new ArgumentException($"Invalid format for color string: {value}. Expected format is 'label|hexcode'.");
    }
}
