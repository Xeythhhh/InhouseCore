using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result.Base;

namespace Domain.Errors;
// TODO Specific Exceptions
public static class DomainErrors
{
    public abstract class DomainError<TException>() : Error(ErrorMessageTemplate)
        where TException : Exception
    {
        public static string ErrorMessageTemplate = "An Error occurred.";

        public static TException Exception(IResultBase result) => Exception(string.Join("\n", result.Reasons.Select(r => r.Message)));
        public static TException Exception(string? message = null) => (TException)Activator.CreateInstance(typeof(TException), message ?? ErrorMessageTemplate)!;
    }

    public sealed class OutOfRangeError : DomainError<Exception>
    {
        public OutOfRangeError()
        {
            ErrorMessageTemplate = "Value out of range.";
        }
    }

    public sealed class InvalidValueError : DomainError<Exception>
    {
        public InvalidValueError()
        {
            ErrorMessageTemplate = "Invalid value.";
        }
    }

    public sealed class InvalidValueForImplicitConversionError : DomainError<Exception>
    {
        public InvalidValueForImplicitConversionError()
        {
            ErrorMessageTemplate = "Invalid value for implicit conversion.";
        }
    }

    public sealed class NullOrEmptyError : DomainError<Exception>
    {
        public NullOrEmptyError()
        {
            ErrorMessageTemplate = "Value was null or empty.";
        }
    }
}
