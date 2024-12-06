namespace SharedKernel.Primitives.Reasons;

/// <summary>Represents an error, extending <see cref="IReason"/> with additional error-specific details.</summary>
public interface IError : IReason
{
    /// <summary>The underlying reasons that contributed to this error.</summary>
    List<IError> Reasons { get; }
}
