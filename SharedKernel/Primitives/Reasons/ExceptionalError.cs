using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Reasons;
/// <summary>Error class which stores additionally the exception.</summary>
public class ExceptionalError(string message, Exception exception) : Error(message), IExceptionalError
{
    /// <summary>Exception of the error</summary>
    public Exception Exception { get; } = exception;

    public ExceptionalError(Exception exception)
        : this(exception.Message, exception)
    { }

    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .WithInfo(nameof(Reasons), ResultBase.ErrorReasonsToString(Reasons))
            .WithInfo(nameof(Exception), Exception.ToString())
            .Build();
}
