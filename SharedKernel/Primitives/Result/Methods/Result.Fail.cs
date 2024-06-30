using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Creates a failed result with the given error</summary>
    public static Result Fail(IError error)
    {
        Result result = new();
        result.WithError(error);
        return result;
    }

    /// <summary>Creates a failed result with the given error message. Internally an error object from the error factory is created. </summary>
    public static Result Fail(string errorMessage)
    {
        Result result = new();
        result.WithError(Error.DefaultFactory(errorMessage));
        return result;
    }

    /// <summary>Creates a failed result with the given error messages. Internally a list of error objects from the error factory is created</summary>
    public static Result Fail(IEnumerable<string> errorMessages)
    {
        if (errorMessages == null)
            throw new ArgumentNullException(nameof(errorMessages), "The list of error messages cannot be null");

        Result result = new();
        result.WithErrors(errorMessages.Select(Error.DefaultFactory));
        return result;
    }

    /// <summary>Creates a failed result with the given errors.</summary>
    public static Result Fail(IEnumerable<IError> errors)
    {
        if (errors == null)
            throw new ArgumentNullException(nameof(errors), "The list of errors cannot be null");

        Result result = new();
        result.WithErrors(errors);
        return result;
    }

    /// <summary>Creates a failed result with the given error</summary>
    public static Result<TValue> Fail<TValue>(IError error)
    {
        Result<TValue> result = new();
        result.WithError(error);
        return result;
    }

    /// <summary>Creates a failed result with the given error message. Internally an error object from the error factory is created. </summary>
    public static Result<TValue> Fail<TValue>(string errorMessage)
    {
        Result<TValue> result = new();
        result.WithError(Error.DefaultFactory(errorMessage));
        return result;
    }

    /// <summary>Creates a failed result with the given error messages. Internally a list of error objects from the error factory is created. </summary>
    public static Result<TValue> Fail<TValue>(IEnumerable<string> errorMessages)
    {
        if (errorMessages == null)
            throw new ArgumentNullException(nameof(errorMessages), "The list of error messages cannot be null");

        Result<TValue> result = new();
        result.WithErrors(errorMessages.Select(Error.DefaultFactory));
        return result;
    }

    /// <summary>Creates a failed result with the given errors.</summary>
    public static Result<TValue> Fail<TValue>(IEnumerable<IError> errors)
    {
        if (errors == null)
            throw new ArgumentNullException(nameof(errors), "The list of errors cannot be null");

        Result<TValue> result = new();
        result.WithErrors(errors);
        return result;
    }
}
