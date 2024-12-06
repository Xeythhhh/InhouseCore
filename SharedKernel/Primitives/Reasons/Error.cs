using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Reasons;

/// <summary>Represents an error that causes a result to fail. Errors can include metadata and nested reasons for additional context.</summary>
public class Error : IError
{
    internal static Func<string, IError> DefaultFactory => errorMessage => new Error(errorMessage);
    internal static Func<Exception, IError> DefaultExceptionalErrorFactory => exception => new ExceptionalError(exception.Message, exception);

    /// <summary>The message describing the error.</summary>
    public string Message { get; protected set; } = "Missing Error Message.";

    /// <summary>Metadata providing additional context about the error.</summary>
    public Dictionary<string, object> Metadata { get; }

    /// <summary>The underlying reasons contributing to this error.</summary>
    public List<IError> Reasons { get; }

    /// <summary>Initializes a new instance of the <see cref="Error"/> class.</summary>
    protected Error()
    {
        Metadata = new();
        Reasons = new();
    }

    /// <summary>Initializes a new instance of the <see cref="Error"/> class with a specific message.</summary>
    /// <param name="message">The error message.</param>
    public Error(string message) : this() => Message = message;

    /// <summary>Initializes a new instance of the <see cref="Error"/> class with a message and a root cause.</summary>
    /// <param name="message">The error message.</param>
    /// <param name="causedBy">The root cause of the error.</param>
    public Error(string message, IError causedBy) : this(message)
    {
        ArgumentNullException.ThrowIfNull(causedBy);
        Reasons.Add(causedBy);
    }

    /// <summary>Adds a root cause to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(IError)"/> method.</summary>
    /// <param name="error">The root cause to add.</param>
    public Error CausedBy(IError error) => CausedBy<Error>(error);

    /// <summary>Adds an exception as a root cause to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(Exception)"/> method.</summary>
    /// <param name="exception">The exception to add as a root cause.</param>
    public Error CausedBy(Exception exception) => CausedBy<Error>(exception);

    /// <summary>Adds a message as a root cause to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(string)"/> method.</summary>
    /// <param name="message">The error message to add as a root cause.</param>
    public Error CausedBy(string message) => CausedBy<Error>(message);

    /// <summary>Adds multiple errors as root causes to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(IEnumerable{IError})"/> method.</summary>
    /// <param name="errors">The errors to add as root causes.</param>
    public Error CausedBy(IEnumerable<IError> errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple errors as root causes to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(IEnumerable{IError})"/> method.</summary>
    /// <param name="errors">The errors to add as root causes.</param>
    public Error CausedBy(params IError[] errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple messages as root causes to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(IEnumerable{string})"/> method.</summary>
    /// <param name="errors">The error messages to add as root causes.</param>
    public Error CausedBy(IEnumerable<string> errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple messages as root causes to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="CausedBy{TError}(IEnumerable{string})"/> method.</summary>
    /// <param name="errors">The error messages to add as root causes.</param>
    public Error CausedBy(params string[] errors) => CausedBy<Error>(errors);

    /// <summary>Adds metadata to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="WithMetadata{TError}(string, object)"/> method.</summary>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    public Error WithMetadata(string metadataName, object metadataValue) => WithMetadata<Error>(metadataName, metadataValue);

    /// <summary>Adds multiple metadata items to this error and returns the current instance as <see cref="Error"/>.
    /// This method delegates to the generic <see cref="WithMetadata{TError}(Dictionary{string, object})"/> method.</summary>
    /// <param name="metadata">The metadata to add.</param>
    public Error WithMetadata(Dictionary<string, object> metadata) => WithMetadata<Error>(metadata);

    /// <summary>Adds a root cause to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="error">The root cause to add.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(IError error) where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);
        Reasons.Add(error);
        return (TError)this;
    }

    /// <summary>Adds an exception as a root cause to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="exception">The exception to add as a root cause.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(Exception exception) where TError : Error
    {
        ArgumentNullException.ThrowIfNull(exception);
        Reasons.Add(DefaultExceptionalErrorFactory(exception));
        return (TError)this;
    }

    /// <summary>Adds a message as a root cause to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="message">The error message to add as a root cause.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(string message) where TError : Error
    {
        Reasons.Add(DefaultFactory(message));
        return (TError)this;
    }

    /// <summary>Adds multiple errors as root causes to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="errors">The errors to add as root causes.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(IEnumerable<IError> errors) where TError : Error
    {
        ArgumentNullException.ThrowIfNull(errors);
        Reasons.AddRange(errors);
        return (TError)this;
    }

    /// <summary>Adds multiple messages as root causes to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="errors">The error messages to add as root causes.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(IEnumerable<string> errors) where TError : Error
    {
        ArgumentNullException.ThrowIfNull(errors);
        Reasons.AddRange(errors.Select(DefaultFactory));
        return (TError)this;
    }

    /// <summary>Adds metadata to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError WithMetadata<TError>(string metadataName, object metadataValue) where TError : Error
    {
        Metadata[metadataName] = metadataValue;
        return (TError)this;
    }

    /// <summary>Adds multiple metadata items to this error and returns the current instance.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="metadata">The metadata to add.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError WithMetadata<TError>(Dictionary<string, object> metadata) where TError : Error
    {
        foreach (var (key, value) in metadata)
        {
            Metadata[key] = value;
        }

        return (TError)this;
    }

    /// <summary>Returns a string representation of this error, including its type, message, metadata, and reasons.</summary>
    /// <returns>A string describing this error.</returns>
    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .WithInfo(nameof(Reasons), ResultBase.ErrorReasonsToString(Reasons))
            .Build();
}
