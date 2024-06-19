namespace Presentation.Discord.Exceptions;

/// <summary><see cref="DiscordApplicationException"/> thrown when the Discord application does not have access to any channel.</summary>
internal sealed class NoDiscordChannelsException :
    DiscordApplicationException
{
    /// <summary>Initializes a new instance of the <see cref="NoDiscordChannelsException"/> class with a default message.</summary>
    public NoDiscordChannelsException() : base("Discord Application does not have access to any channel.") { }

    /// <summary>Initializes a new instance of the <see cref="NoDiscordChannelsException"/> class with a specified error message.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NoDiscordChannelsException(string? message) : base(message) { }

    /// <summary>Initializes a new instance of the <see cref="NoDiscordChannelsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public NoDiscordChannelsException(string? message, Exception? innerException) : base(message, innerException) { }
}