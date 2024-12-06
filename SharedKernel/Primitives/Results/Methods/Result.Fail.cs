using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;

/// <summary>Provides factory methods for creating failed results.</summary>
public partial class Result
{
    /// <summary>Creates a failed <see cref="Result"/> with the specified error.</summary>
    public static Result Fail(IError error) =>
        new Result().WithError(error);

    /// <summary>Creates a failed <see cref="Result"/> with the specified error message.</summary>
    public static Result Fail(string errorMessage) =>
        new Result().WithError(Error.DefaultFactory(errorMessage));

    /// <summary>Creates a failed <see cref="Result"/> with the specified error messages.</summary>
    public static Result Fail(IEnumerable<string> errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);
        return new Result().WithErrors(errorMessages.Select(Error.DefaultFactory));
    }

    /// <summary>Creates a failed <see cref="Result"/> with the specified errors.</summary>
    public static Result Fail(IEnumerable<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new Result().WithErrors(errors);
    }

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified error.</summary>
    public static Result<TValue> Fail<TValue>(IError error) =>
        new Result<TValue>().WithError(error);

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified error message.</summary>
    public static Result<TValue> Fail<TValue>(string errorMessage) =>
        new Result<TValue>().WithError(Error.DefaultFactory(errorMessage));

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified error messages.</summary>
    public static Result<TValue> Fail<TValue>(IEnumerable<string> errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);
        return new Result<TValue>().WithErrors(errorMessages.Select(Error.DefaultFactory));
    }

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified errors.</summary>
    public static Result<TValue> Fail<TValue>(IEnumerable<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new Result<TValue>().WithErrors(errors);
    }
}
