namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result Ensure(Func<bool> predicate, string errorMessage) =>
        IsFailed ? this
            : predicate() ? this
                : Fail(errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result> Ensure(Func<Task<bool>> predicate, string errorMessage) =>
        IsFailed ? this
            : await predicate() ? this
                : Fail(errorMessage);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result Ensure(Func<Result> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = predicate();
        return predicateResult.IsFailed ? Fail(predicateResult.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result Ensure<TValue>(Func<Result<TValue>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> predicateResult = predicate();
        return predicateResult.IsFailed ? predicateResult.ToResult() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result> Ensure(Func<Task<Result>> predicate)
    {
        if (IsFailed) return this;
        Result predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result> Ensure<TValue>(Func<Task<Result<TValue>>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult.ToResult() : this;
    }
}
