using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public abstract class ResultBase<TResult> : ResultBase
    where TResult : ResultBase<TResult>
{
    /// <summary>Add an error</summary>
    public TResult WithError(string errorMessage) =>
        WithError(Error.DefaultFactory(errorMessage));

    /// <summary>Add an error</summary>
    public TResult WithError(IError error) =>
        WithReason(error);

    /// <summary>Add an error</summary>
    public TResult WithError<TError>()
        where TError : IError, new() =>
        WithError(new TError());

    /// <summary>Add multiple errors</summary>
    public TResult WithErrors(IEnumerable<IError> errors) =>
        WithReasons(errors);

    /// <summary>Add multiple errors</summary>
    public TResult WithErrors(IEnumerable<string> errors) =>
        WithReasons(errors.Select(errorMessage => Error.DefaultFactory(errorMessage)));

    /// <summary>Add a reason (success or error)</summary>
    public TResult WithReason(IReason reason)
    {
        Reasons.Add(reason);
        return (TResult)this;
    }

    /// <summary>Add multiple reasons (success or error)</summary>
    public TResult WithReasons(IEnumerable<IReason> reasons)
    {
        Reasons.AddRange(reasons);
        return (TResult)this;
    }

    /// <summary>Add a success</summary>
    public TResult WithSuccess(Func<ISuccess> successFactory) =>
        WithSuccess(successFactory());

    /// <summary>Add a success</summary>
    public TResult WithSuccess(string successMessage) =>
        WithSuccess(Success.DefaultFactory(successMessage));

    /// <summary>Add a success</summary>
    public TResult WithSuccess(ISuccess success) =>
        WithReason(success);

    /// <summary>Add a success</summary>
    public TResult WithSuccess<TSuccess>()
        where TSuccess : Success, new() =>
        WithSuccess(new TSuccess());

    public TResult WithSuccesses(IEnumerable<ISuccess> successes)
    {
        foreach (ISuccess success in successes)
            WithSuccess(success);

        return (TResult)this;
    }
}
