namespace Presentation.Discord.Exceptions;

/// <summary><see cref="DiscordApplicationException"/> thrown when the Discord Gateway Token is missing from configuration.</summary>
internal sealed class MissingDiscordGatewayTokenException :
    DiscordApplicationException
{
    /// <summary>Initializes a new instance of the <see cref="MissingDiscordGatewayTokenException"/> class with a default message.</summary>
    public MissingDiscordGatewayTokenException() : base("Discord Gateway Token not found in configuration.") { }

    /// <summary>Initializes a new instance of the <see cref="MissingDiscordGatewayTokenException"/> class with a specified error message.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public MissingDiscordGatewayTokenException(string? message) : base(message) { }

    /// <summary>Initializes a new instance of the <see cref="MissingDiscordGatewayTokenException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public MissingDiscordGatewayTokenException(string? message, Exception? innerException) : base(message, innerException) { }
}
