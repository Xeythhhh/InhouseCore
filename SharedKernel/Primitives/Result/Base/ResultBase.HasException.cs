using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;
public abstract partial class ResultBase
{
    /// <summary>Check if the result object contains an exception from a specific type</summary>
    public bool HasException<TException>()
        where TException : Exception =>
        HasException<TException>(out _);

    /// <summary>Check if the result object contains an exception from a specific type</summary>
    public bool HasException<TException>(out IEnumerable<IError> result)
        where TException : Exception =>
        HasException<TException>(_ => true, out result);

    /// <summary>Check if the result object contains an exception from a specific type and with a specific condition</summary>
    public bool HasException<TException>(Func<TException, bool> predicate)
        where TException : Exception =>
        HasException(predicate, out _);

    /// <summary>Check if the result object contains an exception from a specific type and with a specific condition</summary>
    public bool HasException<TException>(Func<TException, bool> predicate, out IEnumerable<IError> result)
        where TException : Exception
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return HasException(Errors, predicate, out result);
    }

    private static bool HasException<TException>(
        List<IError> errors,
        Func<TException, bool> predicate,
        out IEnumerable<IError> result)
        where TException : Exception
    {
        List<ExceptionalError> foundErrors = errors.OfType<ExceptionalError>().Where(e =>
                e.Exception is TException rootExceptionOfTException
                && predicate(rootExceptionOfTException))
            .ToList();

        if (foundErrors.Count != 0)
        {
            result = foundErrors;
            return true;
        }

        foreach (IError error in errors)
        {
            if (HasException(error.Reasons, predicate, out IEnumerable<IError>? fErrors))
            {
                result = fErrors;
                return true;
            }
        }

        result = Array.Empty<IError>();
        return false;
    }
}