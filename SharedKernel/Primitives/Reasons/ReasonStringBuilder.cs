using SharedKernel.Extensions;

namespace SharedKernel.Primitives.Reasons;

/// <summary>Builds a string representation of a reason, including its type and associated information.</summary>
internal class ReasonStringBuilder
{
    private string _reasonType = string.Empty;
    private readonly List<string> _infos = new();

    /// <summary>Sets the type of the reason.</summary>
    /// <param name="type">The <see cref="Type"/> of the reason.</param>
    /// <returns>The current instance of <see cref="ReasonStringBuilder"/>.</returns>
    public ReasonStringBuilder WithReasonType(Type type)
    {
        _reasonType = type.Name;
        return this;
    }

    /// <summary>Adds labeled information to the reason.</summary>
    /// <param name="label">The label for the information.</param>
    /// <param name="value">The value associated with the label.</param>
    /// <returns>The current instance of <see cref="ReasonStringBuilder"/>.</returns>
    public ReasonStringBuilder WithInfo(string label, string value)
    {
        string infoString = value.ToLabelValueStringOrEmpty(label);

        if (!string.IsNullOrEmpty(infoString))
        {
            _infos.Add(infoString);
        }

        return this;
    }

    /// <summary>Builds the string representation of the reason.</summary>
    /// <returns>A string describing the reason, including its type and associated information.</returns>
    public string Build() => _reasonType + (_infos.Count > 0 ? " with " + ReasonInfosToString(_infos) : string.Empty);

    /// <summary>Converts a list of reason information strings into a single string.</summary>
    /// <param name="reasonInfos">The list of reason information strings.</param>
    /// <returns>A comma-separated string of reason information.</returns>
    private static string ReasonInfosToString(List<string> reasonInfos) =>
        string.Join(", ", reasonInfos);
}
