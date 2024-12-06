using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Checks if the result contains an error of type <typeparamref name="TError"/>.</summary>
    public bool HasError<TError>() where TError : IError => HasError<TError>(out _);

    /// <summary>Checks if the result contains an error of type <typeparamref name="TError"/> and outputs the matching errors.</summary>
    public bool HasError<TError>(out IEnumerable<TError> result) where TError : IError =>
        HasError(_ => true, out result);

    /// <summary>Checks if the result contains an error of type <typeparamref name="TError"/> that matches the specified condition.</summary>
    /// <param name="predicate">The condition to evaluate for each error of type <typeparamref name="TError"/>.</param>
    public bool HasError<TError>(Func<TError, bool> predicate) where TError : IError => HasError(predicate, out _);

    /// <summary>Checks if the result contains an error of type <typeparamref name="TError"/> that matches the specified condition
    /// and outputs the matching errors.</summary>
    /// <param name="predicate">The condition to evaluate for each error of type <typeparamref name="TError"/>.</param>
    public bool HasError<TError>(Func<TError, bool> predicate, out IEnumerable<TError> result) where TError : IError
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return CheckForError(Errors, predicate, out result);
    }

    /// <summary>Checks if the result contains any error that matches the specified condition.</summary>
    /// <param name="predicate">The condition to evaluate for each error.</param>
    public bool HasError(Func<IError, bool> predicate) => HasError(predicate, out _);

    /// <summary>Checks if the result contains any error that matches the specified condition and outputs the matching errors.</summary>
    /// <param name="predicate">The condition to evaluate for each error.</param>
    public bool HasError(Func<IError, bool> predicate, out IEnumerable<IError> result)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return CheckForError(Errors, predicate, out result);
    }

    /// <summary>Gets all errors of type <typeparamref name="TError"/>.</summary>
    public IEnumerable<TError> GetErrors<TError>() where TError : IError => Errors.OfType<TError>();

    /// <summary>Gets all errors that match the specified condition.</summary>
    /// <param name="predicate">The condition to evaluate for each error.</param>
    public IEnumerable<IError> GetErrors(Func<IError, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return Errors.Where(predicate);
    }

    /// <summary>Recursively checks if a collection of <see cref="IError"/> contains an error of type <typeparamref name="T"/>
    /// that matches the specified condition.</summary>
    private static bool CheckForError<T>(
        IEnumerable<IError> errors,
        Func<T, bool> predicate,
        out IEnumerable<T> result) where T : IError
    {
        List<T> foundErrors = errors.OfType<T>().Where(predicate).ToList();
        if (foundErrors.Count != 0)
        {
            result = foundErrors;
            return true;
        }

        foreach (IError error in errors)
        {
            if (CheckForError(error.Reasons, predicate, out IEnumerable<T>? nestedErrors))
            {
                result = nestedErrors;
                return true;
            }
        }

        result = Enumerable.Empty<T>();
        return false;
    }
}
