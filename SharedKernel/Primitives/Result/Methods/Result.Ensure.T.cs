using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;

public partial class Result<TValue>
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure<TError>(Func<TValue, bool> predicate, Func<TValue, TError> errorPredicate)
        where TError : IError => IsFailed ? this
            : predicate(Value) ? this
                : Result.Fail<TValue>(errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure<TError>(Func<TValue, bool> predicate, TError error)
        where TError : IError =>
            Ensure(predicate, _ => error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, string errorMessage)
        => Ensure(predicate, new Error(errorMessage));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, Func<TValue, string> errorPredicate)
        => Ensure(predicate, _ => new Error(errorPredicate(Value)));

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure(Func<Result> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = predicate();
        return predicateResult.IsFailed ? Result.Fail<TValue>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure(Func<Result<TValue>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> predicateResult = predicate();
        return predicateResult.IsFailed ? Result.Fail<TValue>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure(Func<TValue, Result> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = predicate(Value);
        return predicateResult.IsFailed ? Result.Fail<TValue>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<TValue> Ensure(Func<TValue, Result<TValue>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> predicateResult = predicate(Value);
        return predicateResult.IsFailed ? Result.Fail<TValue>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, string errorMessage) =>
        IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<TValue>(errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure<TError>(Func<TValue, Task<bool>> predicate, TError error)
        where TError : IError => IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<TValue>(error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure<TError>(Func<TValue, Task<bool>> predicate, Func<TValue, TError> errorPredicate)
        where TError : IError => IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<TValue>(errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure<TError>(Func<TValue, Task<bool>> predicate, Func<TValue, Task<TError>> errorPredicate)
        where TError : IError => IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<TValue>(await errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, string> errorPredicate) =>
        IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<TValue>(errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Task<string>> errorPredicate) =>
        IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<TValue>(await errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<Task<Result>> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult.ToResult<TValue>() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<Task<Result<TValue>>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<Result>> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = await predicate(Value);
        return predicateResult.IsFailed ? predicateResult.ToResult<TValue>() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<Result<TValue>>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> predicateResult = await predicate(Value);
        return predicateResult.IsFailed ? predicateResult : this;
    }
}
