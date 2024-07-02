using Application.Abstractions;

namespace Api;
public sealed record ReadConnectionString : IReadConnectionString
{
    public ReadConnectionString(string value)
    {
        Value = value;
    }
    public string Value { get; init; }
}
