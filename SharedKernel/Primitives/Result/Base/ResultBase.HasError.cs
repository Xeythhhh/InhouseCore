using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;
public abstract partial class ResultBase
{
    /// <summary>Check if the result object contains an error from a specific type</summary>
    public bool HasError<TError>() where TError : IError =>
        HasError<TError>(out _);

    /// <summary>Check if the result object contains an error from a specific type</summary>
    public bool HasError<TError>(out IEnumerable<TError> result)
        where TError : IError =>
        HasError(_ => true, out result);

    /// <summary>Check if the result object contains an error from a specific type and with a specific condition</summary>
    public bool HasError<TError>(Func<TError, bool> predicate)
        where TError : IError =>
        HasError(predicate, out _);

    /// <summary>Check if the result object contains an error from a specific type and with a specific condition</summary>
    public bool HasError<TError>(Func<TError, bool> predicate, out IEnumerable<TError> result)
        where TError : IError
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return HasError(Errors, predicate, out result);
    }

    /// <summary>Check if the result object contains an error with a specific condition</summary>
    public bool HasError(Func<IError, bool> predicate) => HasError(predicate, out _);

    /// <summary>Check if the result object contains an error with a specific condition</summary>
    public bool HasError(Func<IError, bool> predicate, out IEnumerable<IError> result)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return HasError(Errors, predicate, out result);
    }

    private static bool HasError<TError>(
        List<IError> errors,
        Func<TError, bool> predicate,
        out IEnumerable<TError> result)
        where TError : IError
    {
        List<TError> foundErrors = errors.OfType<TError>().Where(predicate).ToList();
        if (foundErrors.Count != 0)
        {
            result = foundErrors;
            return true;
        }

        foreach (IError error in errors)
        {
            if (HasError(error.Reasons, predicate, out IEnumerable<TError>? fErrors))
            {
                result = fErrors;
                return true;
            }
        }

        result = Array.Empty<TError>();
        return false;
    }
}
