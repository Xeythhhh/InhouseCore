using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Reasons;

/// <summary>Represents an error that includes an associated <see cref="Exception"/> for additional context.</summary>
public class ExceptionalError(string message, Exception exception) : Error(message), IExceptionalError
{
    /// <summary>The <see cref="Exception"/> associated with this error.</summary>
    public Exception Exception { get; } = exception;

    /// <summary>Initializes a new instance of the <see cref="ExceptionalError"/> class with a specific <see cref="Exception"/>.</summary>
    /// <param name="exception">The exception to associate with this error.</param>
    public ExceptionalError(Exception exception)
        : this(exception.Message, exception)
    { }

    /// <summary>Returns a string representation of this error, including the exception details.</summary>
    /// <returns>A string describing this error.</returns>
    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .WithInfo(nameof(Reasons), ResultBase.ErrorReasonsToString(Reasons))
            .WithInfo(nameof(Exception), Exception.ToString())
            .Build();
}
