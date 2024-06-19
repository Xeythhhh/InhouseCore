namespace Domain.Primitives;

/// <summary>Represents a concrete domain error.</summary>
/// <param name="Code">Gets the error code</param>
/// <param name="Message">Gets the error message</param>
public sealed record Error(string Code, string Message) :
    ValueObject
{
    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }

    /// <summary>Gets the empty error instance.</summary>
    internal static Error None => new(string.Empty, string.Empty);
}
