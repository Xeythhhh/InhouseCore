using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Reasons;
/// <summary>Objects from Error class cause a failed result</summary>
public class Error : IError
{
    internal static Func<string, IError> DefaultFactory => (string errorMessage) => new Error(errorMessage);
    internal static Func<Exception, IError> DefaultExceptionalErrorFactory =>
        (Exception exception) => new ExceptionalError(exception.Message, exception);

    /// <summary>Message of the error</summary>
    public string Message { get; protected set; } = "Missing Error Message.";

    /// <summary>Metadata of the error</summary>
    public Dictionary<string, object> Metadata { get; }

    /// <summary>Get the reasons of an error</summary>
    public List<IError> Reasons { get; }

    protected Error()
    {
        Metadata = new Dictionary<string, object>();
        Reasons = new List<IError>();
    }

    /// <summary>Creates a new instance of <see cref="Error"/></summary>
    /// <param name="message">Description of the error</param>
    public Error(string message) : this() => Message = message;

    /// <summary>Creates a new instance of <see cref="Error"/></summary>
    /// <param name="message">Description of the error</param>
    /// <param name="causedBy">The root cause of the <see cref="Error"/></param>
    public Error(string message, IError causedBy)
        : this(message)
    {
        ArgumentNullException.ThrowIfNull(causedBy);
        Reasons.Add(causedBy);
    }

    /// <summary>Set the root cause of the error</summary>
    public Error CausedBy(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        Reasons.Add(error);
        return this;
    }

    /// <summary>Set the root cause of the error</summary>
    public Error CausedBy(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        Reasons.Add(DefaultExceptionalErrorFactory(exception));
        return this;
    }

    /// <summary>Set the root cause of the error</summary>
    public Error CausedBy(string message)
    {
        Reasons.Add(DefaultFactory(message));
        return this;
    }

    /// <summary>Set the root cause of the error</summary>
    public Error CausedBy(IEnumerable<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        Reasons.AddRange(errors);
        return this;
    }

    /// <summary>Set the root cause of the error</summary>
    public Error CausedBy(IEnumerable<string> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        Reasons.AddRange(errors.Select(errorMessage => DefaultFactory(errorMessage)));
        return this;
    }

    /// <summary>Set the metadata</summary>
    public Error WithMetadata(string metadataName, object metadataValue)
    {
        Metadata.Add(metadataName, metadataValue);
        return this;
    }

    /// <summary>Set the metadata</summary>
    public Error WithMetadata(Dictionary<string, object> metadata)
    {
        foreach (KeyValuePair<string, object> metadataItem in metadata)
        {
            Metadata.Add(metadataItem.Key, metadataItem.Value);
        }

        return this;
    }

    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .WithInfo(nameof(Reasons), ResultBase.ErrorReasonsToString(Reasons))
            .Build();
}
