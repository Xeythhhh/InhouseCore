using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<bool> predicate, string errorMessage) =>
        (await resultTask).Ensure(predicate, errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Task<bool>> predicate, string errorMessage)
    {
        Result result = await resultTask;
        return result.IsFailed ? result
            : await predicate() ? result
                : Result.Fail(errorMessage);
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Result> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Task<Result>> predicate)
    {
        Result result = await resultTask;
        if (result.IsFailed) return result;
        Result predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult : result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure<TValue>(this Task<Result> resultTask, Func<Task<Result<TValue>>> predicate)
    {
        Result result = await resultTask;
        if (result.IsFailed) return result;
        Result<TValue> predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult.ToResult() : result;
    }
}
