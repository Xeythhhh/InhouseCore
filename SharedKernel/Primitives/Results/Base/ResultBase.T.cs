using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public abstract class ResultBase<TResult> : ResultBase where TResult : ResultBase<TResult>
{
    /// <summary>Adds an error to the result.</summary>
    public TResult WithError(string errorMessage) => WithError(Error.DefaultFactory(errorMessage));

    /// <summary>Adds an error to the result.</summary>
    public TResult WithError(IError error) => WithReason(error);

    /// <summary>Adds an error of a specific type to the result.</summary>
    public TResult WithError<TError>() where TError : IError, new() => WithError(new TError());

    /// <summary>Adds multiple errors to the result.</summary>
    public TResult WithErrors(IEnumerable<IError> errors) => WithReasons(errors);

    /// <summary>Adds multiple error messages to the result.</summary>
    public TResult WithErrors(IEnumerable<string> errorMessages) =>
        WithReasons(errorMessages.Select(Error.DefaultFactory));

    /// <summary>Adds a reason (success or error) to the result.</summary>
    public TResult WithReason(IReason reason)
    {
        ArgumentNullException.ThrowIfNull(reason);
        Reasons.Add(reason);
        return (TResult)this;
    }

    /// <summary>Adds multiple reasons (success or error) to the result.</summary>
    public TResult WithReasons(IEnumerable<IReason> reasons)
    {
        ArgumentNullException.ThrowIfNull(reasons);
        Reasons.AddRange(reasons);
        return (TResult)this;
    }

    /// <summary>Adds a success to the result using a factory.</summary>
    public TResult WithSuccess(Func<ISuccess> successFactory) => WithSuccess(successFactory());

    /// <summary>Adds a success message to the result.</summary>
    public TResult WithSuccess(string successMessage) => WithSuccess(Success.DefaultFactory(successMessage));

    /// <summary>Adds a success to the result.</summary>
    public TResult WithSuccess(ISuccess success) => WithReason(success);

    /// <summary>Adds a success of a specific type to the result.</summary>
    public TResult WithSuccess<TSuccess>() where TSuccess : Success, new() => WithSuccess(new TSuccess());

    /// <summary>Adds multiple successes to the result.</summary>
    public TResult WithSuccesses(IEnumerable<ISuccess> successes)
    {
        ArgumentNullException.ThrowIfNull(successes);
        foreach (ISuccess success in successes)
            WithSuccess(success);

        return (TResult)this;
    }
}
