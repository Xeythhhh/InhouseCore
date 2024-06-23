namespace Domain.Errors;
public class DomainError
{
    protected static string NullOrEmpty(string targetName) => $"'{targetName}' can not be null or empty.";
    protected static string Null(string targetName) => $"'{targetName}' can not be null.";
    protected static string OutOfRange(string targetName) => $"'{targetName}' value out of range";
}
