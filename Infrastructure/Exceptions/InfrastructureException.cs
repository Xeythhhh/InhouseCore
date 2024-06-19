namespace Infrastructure.Exceptions;

/// <summary> Base class for all exceptions specific to the Infrastructure layer. </summary>
internal abstract class InfrastructureException :
    Exception
{
    /// <summary> Initializes a new instance of the <see cref="InfrastructureException"/> class. </summary>
    protected InfrastructureException() { }

    /// <summary> Initializes a new instance of the <see cref="InfrastructureException"/> class with a specified error message. </summary>
    /// <param name="message"> The error message that explains the reason for the exception. </param>
    protected InfrastructureException(string? message) : base(message) { }

    /// <summary> Initializes a new instance of the <see cref="InfrastructureException"/> class with a specified error message and inner exception. </summary>
    /// <param name="message"> The error message that explains the reason for the exception. </param>
    /// <param name="innerException"> The exception that is the cause of the current exception. </param>
    protected InfrastructureException(string? message, Exception? innerException) : base(message, innerException) { }
}
