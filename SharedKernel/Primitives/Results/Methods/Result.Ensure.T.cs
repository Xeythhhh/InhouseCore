using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;

public partial class Result<T>
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<T> Ensure<TError>(Func<T, bool> predicate, Func<T, TError> errorPredicate)
        where TError : IError => IsFailed ? this
            : predicate(Value) ? this
                : Result.Fail<T>(errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<T> Ensure<TError>(Func<T, bool> predicate, TError error)
        where TError : IError =>
            Ensure(predicate, _ => error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<T> Ensure(Func<T, bool> predicate, string errorMessage)
        => Ensure(predicate, new Error(errorMessage));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result<T> Ensure(Func<T, bool> predicate, Func<T, string> errorPredicate)
        => Ensure(predicate, _ => new Error(errorPredicate(Value)));

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<T> Ensure(Func<Result> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = predicate();
        return predicateResult.IsFailed ? Result.Fail<T>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<T> Ensure(Func<Result<T>> predicate)
    {
        if (IsFailed) return this;
        Result<T> predicateResult = predicate();
        return predicateResult.IsFailed ? Result.Fail<T>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<T> Ensure(Func<T, Result> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = predicate(Value);
        return predicateResult.IsFailed ? Result.Fail<T>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result<T> Ensure(Func<T, Result<T>> predicate)
    {
        if (IsFailed) return this;
        Result<T> predicateResult = predicate(Value);
        return predicateResult.IsFailed ? Result.Fail<T>(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<T, Task<bool>> predicate, string errorMessage) =>
        IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<T>(errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure<TError>(Func<T, Task<bool>> predicate, TError error)
        where TError : IError => IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<T>(error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure<TError>(Func<T, Task<bool>> predicate, Func<T, TError> errorPredicate)
        where TError : IError => IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<T>(errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure<TError>(Func<T, Task<bool>> predicate, Func<T, Task<TError>> errorPredicate)
        where TError : IError => IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<T>(await errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<T, Task<bool>> predicate, Func<T, string> errorPredicate) =>
        IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<T>(errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<T, Task<bool>> predicate, Func<T, Task<string>> errorPredicate) =>
        IsFailed ? this
            : await predicate(Value) ? this
                : Result.Fail<T>(await errorPredicate(Value));

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<Task<Result>> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult.ToResult<T>() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<Task<Result<T>>> predicate)
    {
        if (IsFailed) return this;
        Result<T> predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<T, Task<Result>> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = await predicate(Value);
        return predicateResult.IsFailed ? predicateResult.ToResult<T>() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result<T>> Ensure(Func<T, Task<Result<T>>> predicate)
    {
        if (IsFailed) return this;
        Result<T> predicateResult = await predicate(Value);
        return predicateResult.IsFailed ? predicateResult : this;
    }
}
