using System.Runtime.CompilerServices;

namespace SharedKernel.Primitives.Result;

public partial class Result
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public Result Ensure(Func<bool> predicate, string errorMessage) =>
        IsFailed ? this : predicate() ? this : Fail(errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public async Task<Result> Ensure(Func<Task<bool>> predicate, string errorMessage)
    {
        if (IsFailed) return this;
        bool result = await predicate().ConfigureAwait(false);
        return result ? this : Fail(errorMessage);
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    [OverloadResolutionPriority(1)]
    public async ValueTask<Result> Ensure(Func<ValueTask<bool>> predicate, string errorMessage)
    {
        if (IsFailed) return this;
        bool result = await predicate().ConfigureAwait(false);
        return result ? this : Fail(errorMessage);
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result Ensure(Func<Result> predicate)
    {
        if (IsFailed) return this;
        Result result = predicate();
        return result.IsFailed ? Fail(result.Errors) : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public Result Ensure<TValue>(Func<Result<TValue>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> result = predicate();
        return result.IsFailed ? result.ToResult() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result> Ensure(Func<Task<Result>> predicate)
    {
        if (IsFailed) return this;
        Result result = await predicate().ConfigureAwait(false);
        return result.IsFailed ? result : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    [OverloadResolutionPriority(1)]
    public async ValueTask<Result> Ensure(Func<ValueTask<Result>> predicate)
    {
        if (IsFailed) return this;
        Result result = await predicate().ConfigureAwait(false);
        return result.IsFailed ? result : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public async Task<Result> Ensure<TValue>(Func<Task<Result<TValue>>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> result = await predicate().ConfigureAwait(false);
        return result.IsFailed ? result.ToResult() : this;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    [OverloadResolutionPriority(1)]
    public async ValueTask<Result> Ensure<TValue>(Func<ValueTask<Result<TValue>>> predicate)
    {
        if (IsFailed) return this;
        Result<TValue> result = await predicate().ConfigureAwait(false);
        return result.IsFailed ? result.ToResult() : this;
    }
}
