namespace Presentation.Discord.Exceptions;

/// <summary><see cref="DiscordApplicationException"/> thrown when the Discord application is not a member of any guild.</summary>
internal sealed class NoDiscordGuildsException :
    DiscordApplicationException
{
    /// <summary>Initializes a new instance of the <see cref="NoDiscordGuildsException"/> class with a default message.</summary>
    public NoDiscordGuildsException() : base("Discord Application is not a member of any guild.") { }

    /// <summary>Initializes a new instance of the <see cref="NoDiscordGuildsException"/> class with a specified error message.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NoDiscordGuildsException(string? message) : base(message) { }

    /// <summary>Initializes a new instance of the <see cref="NoDiscordGuildsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public NoDiscordGuildsException(string? message, Exception? innerException) : base(message, innerException) { }
}
