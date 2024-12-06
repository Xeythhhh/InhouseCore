namespace SharedKernel.Primitives.Reasons;

/// <summary>Represents a successful outcome that does not cause a failed result.</summary>
public class Success : ISuccess
{
    internal static Func<string, ISuccess> DefaultFactory => successMessage => new Success(successMessage);

    /// <summary>The message describing the success.</summary>
    public string Message { get; protected set; } = "Missing Success Message.";

    /// <summary>Metadata providing additional context about the success.</summary>
    public Dictionary<string, object> Metadata { get; }

    /// <summary>Initializes a new instance of the <see cref="Success"/> class.</summary>
    protected Success() => Metadata = new();

    /// <summary>Initializes a new instance of the <see cref="Success"/> class with a specific message.</summary>
    /// <param name="message">The success message.</param>
    public Success(string message) : this() => Message = message;

    // Non-Generic Overloads
    /// <summary>Adds metadata to this success and returns the current instance as <see cref="Success"/>.
    /// This method delegates to the generic <see cref="WithMetadata{TSuccess}(string, object)"/> method.</summary>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    public Success WithMetadata(string metadataName, object metadataValue) => WithMetadata<Success>(metadataName, metadataValue);

    /// <summary>Adds multiple metadata items to this success and returns the current instance as <see cref="Success"/>.
    /// This method delegates to the generic <see cref="WithMetadata{TSuccess}(Dictionary{string, object})"/> method.</summary>
    /// <param name="metadata">The metadata to add.</param>
    public Success WithMetadata(Dictionary<string, object> metadata) => WithMetadata<Success>(metadata);

    // Generic Implementations
    /// <summary>Adds metadata to this success and returns the current instance.</summary>
    /// <typeparam name="TSuccess">The type of the current instance.</typeparam>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    /// <returns>The current instance as <typeparamref name="TSuccess"/>.</returns>
    public TSuccess WithMetadata<TSuccess>(string metadataName, object metadataValue) where TSuccess : Success
    {
        Metadata[metadataName] = metadataValue;
        return (TSuccess)this;
    }

    /// <summary>Adds multiple metadata items to this success and returns the current instance.</summary>
    /// <typeparam name="TSuccess">The type of the current instance.</typeparam>
    /// <param name="metadata">The metadata to add.</param>
    /// <returns>The current instance as <typeparamref name="TSuccess"/>.</returns>
    public TSuccess WithMetadata<TSuccess>(Dictionary<string, object> metadata) where TSuccess : Success
    {
        foreach (var (key, value) in metadata)
        {
            Metadata[key] = value;
        }

        return (TSuccess)this;
    }

    /// <summary>Returns a string representation of this success, including its type, message, and metadata.</summary>
    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .Build();
}
