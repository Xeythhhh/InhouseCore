namespace SharedKernel.Primitives.Reasons;

/// <summary>Defines a reason with a message and associated metadata.</summary>
public interface IReason
{
    /// <summary>The message describing the reason.</summary>
    string Message { get; }

    /// <summary>Metadata providing additional context for the reason.</summary>
    Dictionary<string, object> Metadata { get; }
}
