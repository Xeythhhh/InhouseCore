using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, string errorMessage) =>
        resultTask.Ensure(predicate, new Error(errorMessage));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, IError error) =>
        resultTask.Ensure(predicate, _ => error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, Func<TValue, IError> errorPredicate)
    {
        Result<TValue> result = await resultTask;
        return result.IsFailed ? result
            : await predicate(result.Value) ? result
                : Result.Fail<TValue>(errorPredicate(result.Value));
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, Func<TValue, Task<IError>> errorPredicate)
    {
        Result<TValue> result = await resultTask;
        return result.IsFailed ? result
            : await predicate(result.Value) ? result
                : Result.Fail<TValue>(await errorPredicate(result.Value));
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, Func<TValue, string> errorPredicate)
    {
        Result<TValue> result = await resultTask;
        return result.IsFailed ? result
            : await predicate(result.Value) ? result
                : Result.Fail<TValue>(errorPredicate(result.Value));
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, Func<TValue, Task<string>> errorPredicate)
    {
        Result<TValue> result = await resultTask;
        return result.IsFailed ? result
            : await predicate(result.Value) ? result
                : Result.Fail<TValue>(await errorPredicate(result.Value));
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<Task<Result>> predicate)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) return result;
        Result predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult.ToResult<TValue>() : result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<Task<Result<TValue>>> predicate)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) return result;
        Result<TValue> predicateResult = await predicate();
        return predicateResult.IsFailed ? predicateResult : result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<Result>> predicate)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) return result;
        Result predicateResult = await predicate(result.Value);
        return predicateResult.IsFailed ? predicateResult.ToResult<TValue>() : result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<Result<TValue>>> predicate)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) return result;
        Result<TValue> predicateResult = await predicate(result.Value);
        return predicateResult.IsFailed ? predicateResult : result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, bool> predicate, Func<TValue, string> errorPredicate)
    {
        Result<TValue> result = await resultTask;
        return result.IsFailed ? result : result.Ensure(predicate, errorPredicate);
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, bool> predicate, Func<TValue, Task<string>> errorPredicate)
    {
        Result<TValue> result = await resultTask;
        return result.IsFailed ? result
            : predicate(result.Value) ? result
                : Result.Fail<TValue>(await errorPredicate(result.Value));
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, bool> predicate, string errorMessage) =>
        (await resultTask).Ensure(predicate, errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, bool> predicate, IError error) =>
        (await resultTask).Ensure(predicate, error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, bool> predicate, Func<TValue, IError> errorPredicate) =>
        (await resultTask).Ensure(predicate, errorPredicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<Result> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure<TValue>(this Task<Result> resultTask, Func<Result<TValue>> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<Result<TValue>> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Result> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Result<TValue>> predicate) =>
        (await resultTask).Ensure(predicate);
}
