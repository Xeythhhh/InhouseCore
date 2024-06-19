namespace Infrastructure.Exceptions;

/// <summary> Exception thrown when no ValueConverter is registered for a specific type. </summary>
internal class ValueConverterNotRegisteredException :
    InfrastructureException
{
    /// <summary> Initializes a new instance of the <see cref="ValueConverterNotRegisteredException"/> class with the specified type. </summary>
    /// <param name="type"> The type for which no ValueConverter is registered. </param>
    public ValueConverterNotRegisteredException(Type type) :
        base($"No ValueConverter registered for type '{type.AssemblyQualifiedName}'")
    { }

    /// <summary> Initializes a new instance of the <see cref="ValueConverterNotRegisteredException"/> class with a specified error message. </summary>
    /// <param name="message"> The error message that explains the reason for the exception. </param>
    public ValueConverterNotRegisteredException(string? message) : base(message) { }

    /// <summary> Initializes a new instance of the <see cref="ValueConverterNotRegisteredException"/> class with a specified error message and inner exception. </summary>
    /// <param name="message"> The error message that explains the reason for the exception. </param>
    /// <param name="innerException"> The exception that is the cause of the current exception. </param>
    public ValueConverterNotRegisteredException(string? message, Exception? innerException) : base(message, innerException) { }

    /// <summary> Initializes a new instance of the <see cref="ValueConverterNotRegisteredException"/> class. </summary>
    protected ValueConverterNotRegisteredException() { }
}
