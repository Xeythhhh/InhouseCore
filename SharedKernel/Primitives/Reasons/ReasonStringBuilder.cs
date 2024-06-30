using SharedKernel.Extensions.ResultExtensions;

namespace SharedKernel.Primitives.Reasons;

internal class ReasonStringBuilder
{
    private string _reasonType = string.Empty;
    private readonly List<string> _infos = new();

    public ReasonStringBuilder WithReasonType(Type type)
    {
        _reasonType = type.Name;
        return this;
    }

    public ReasonStringBuilder WithInfo(string label, string value)
    {
        string infoString = value.ToLabelValueStringOrEmpty(label);

        if (!string.IsNullOrEmpty(infoString))
        {
            _infos.Add(infoString);
        }

        return this;
    }

    public string Build() =>
        _reasonType + (_infos.Count != 0
            ? " with " + ReasonInfosToString(_infos)
            : string.Empty);

    private static string ReasonInfosToString(List<string> reasonInfos) =>
        string.Join(", ", reasonInfos);
}