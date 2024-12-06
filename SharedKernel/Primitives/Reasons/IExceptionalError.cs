namespace SharedKernel.Primitives.Reasons;

/// <summary> Represents an error that is associated with an <see cref="Exception"/>, 
/// extending <see cref="IError"/> with additional exception-specific details.</summary>
public interface IExceptionalError : IError
{
    /// <summary>The <see cref="Exception"/> associated with this error.</summary>
    Exception Exception { get; }
}
