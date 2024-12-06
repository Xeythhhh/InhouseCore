using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Checks if the result contains an exception of type <typeparamref name="TException"/>.</summary>
    public bool HasException<TException>() where TException : Exception =>
        HasException<TException>(out _);

    /// <summary>Checks if the result contains an exception of type <typeparamref name="TException"/>
    /// and outputs the matching errors.</summary>
    public bool HasException<TException>(out IEnumerable<IError> result) where TException : Exception =>
        HasException<TException>(_ => true, out result);

    /// <summary>Checks if the result contains an exception of type <typeparamref name="TException"/>
    /// that matches the specified condition.</summary>
    /// <param name="predicate">The condition to evaluate for the exception.</param>
    public bool HasException<TException>(Func<TException, bool> predicate) where TException : Exception =>
        HasException(predicate, out _);

    /// <summary>Checks if the result contains an exception of type <typeparamref name="TException"/>
    /// that matches the specified condition and outputs the matching errors.</summary>
    /// <param name="predicate">The condition to evaluate for the exception.</param>
    public bool HasException<TException>(Func<TException, bool> predicate, out IEnumerable<IError> result) where TException : Exception
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return CheckForException(Errors, predicate, out result);
    }

    /// <summary>Recursively checks if a collection of <see cref="IError"/> contains an exception of type <typeparamref name="TException"/>
    /// that matches the specified condition.</summary>
    private static bool CheckForException<TException>(
        IEnumerable<IError> errors,
        Func<TException, bool> predicate,
        out IEnumerable<IError> result) where TException : Exception
    {
        List<ExceptionalError> foundErrors = errors.OfType<ExceptionalError>()
            .Where(e => e.Exception is TException exception && predicate(exception))
            .ToList();

        if (foundErrors.Count != 0)
        {
            result = foundErrors;
            return true;
        }

        foreach (IError error in errors)
        {
            if (CheckForException(error.Reasons, predicate, out IEnumerable<IError>? nestedErrors))
            {
                result = nestedErrors;
                return true;
            }
        }

        result = Enumerable.Empty<IError>();
        return false;
    }

    /// <summary>Retrieves all exceptions of type <typeparamref name="TException"/> from the result's errors.</summary>
    public IEnumerable<ExceptionalError> GetExceptions<TException>() where TException : Exception =>
        Errors.OfType<ExceptionalError>().Where(e => e.Exception is TException);

    /// <summary>Retrieves all exceptions matching the provided condition, without specifying a type.</summary>
    public IEnumerable<IError> GetExceptions(Func<IError, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return Errors.Where(predicate);
    }

    /// <summary>Retrieves exceptions based on metadata value.</summary>
    public IEnumerable<ExceptionalError> GetExceptionsWithMetadata(string metadataKey, object metadataValue) =>
        Errors.OfType<ExceptionalError>().Where(e =>
            e.Metadata.ContainsKey(metadataKey) && e.Metadata[metadataKey].Equals(metadataValue));
}
