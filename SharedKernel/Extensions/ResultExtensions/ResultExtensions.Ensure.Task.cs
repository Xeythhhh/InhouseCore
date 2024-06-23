using FluentResults;

using Error = FluentResults.Error;
#pragma warning disable IDE0046 // Convert to conditional expression

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, string errorMessage) =>
        resultTask.Ensure(predicate, new Error(errorMessage));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Task<Result<T>> Ensure<T, E>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, E error)
        where E : IError => resultTask.Ensure(predicate, _ => error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result./// </summary>
    public static async Task<Result<T>> Ensure<T, E>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, Func<T, E> errorPredicate)
        where E : IError
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        if (!await predicate(result.Value)) return Result.Fail<T>(errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T, E>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, Func<T, Task<E>> errorPredicate)
        where E : IError
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        if (!await predicate(result.Value)) return Result.Fail<T>(await errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, Func<T, string> errorPredicate)
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        if (!await predicate(result.Value)) return Result.Fail<T>(errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, Func<T, Task<string>> errorPredicate)
    {
        Result<T> result = await resultTask;

        if (result.IsFailed) return result;

        if (!await predicate(result.Value)) return Result.Fail<T>(await errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Task<bool>> predicate, string errorMessage)
    {
        Result result = await resultTask;
        if (result.IsFailed) return result;

        if (!await predicate()) return Result.Fail(errorMessage);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Task<Result>> predicate)
    {
        Result result = await resultTask;
        if (result.IsFailed) return result;

        Result predicateResult = await predicate();
        if (predicateResult.IsFailed) return predicateResult;

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<Task<Result>> predicate)
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        Result predicateResult = await predicate();
        if (predicateResult.IsFailed) return predicateResult.ToResult<T>();

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure<T>(this Task<Result> resultTask, Func<Task<Result<T>>> predicate)
    {
        Result result = await resultTask;
        if (result.IsFailed) return result;

        Result<T> predicateResult = await predicate();
        if (predicateResult.IsFailed) return predicateResult.ToResult();

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<Task<Result<T>>> predicate)
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        Result<T> predicateResult = await predicate();
        if (predicateResult.IsFailed) return predicateResult;

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> predicate)
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        Result predicateResult = await predicate(result.Value);
        if (predicateResult.IsFailed) return predicateResult.ToResult<T>();

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Task<Result<T>>> predicate)
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;

        Result<T> predicateResult = await predicate(result.Value);
        if (predicateResult.IsFailed) return predicateResult;

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, string> errorPredicate)
    {
        Result<T> result = await resultTask;
        if (result.IsFailed) return result;
        return result.Ensure(predicate, errorPredicate);
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Task<string>> errorPredicate)
    {
        Result<T> result = await resultTask;

        if (result.IsFailed) return result;
        if (predicate(result.Value)) return result;

        return Result.Fail<T>(await errorPredicate(result.Value));
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, string errorMessage) =>
        (await resultTask).Ensure(predicate, errorMessage);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T, E>(this Task<Result<T>> resultTask, Func<T, bool> predicate, E error)
        where E : IError =>
        (await resultTask).Ensure(predicate, error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T, E>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, E> errorPredicate)
        where E : IError =>
        (await resultTask).Ensure(predicate, errorPredicate);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<bool> predicate, string errorMessage) =>
        (await resultTask).Ensure(predicate, errorMessage);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Result> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<Result> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure<T>(this Task<Result> resultTask, Func<Result<T>> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<Result<T>> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Result> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Result<T>> predicate) =>
        (await resultTask).Ensure(predicate);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<bool>> predicate, string errorMessage)
    {
        if (result.IsFailed) return result;

        if (!await predicate(result.Value))
            return Result.Fail<T>(errorMessage);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T, E>(this Result<T> result, Func<T, Task<bool>> predicate, E error)
        where E : IError
    {
        if (result.IsFailed) return result;

        if (!await predicate(result.Value))
            return Result.Fail<T>(error);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T, E>(this Result<T> result, Func<T, Task<bool>> predicate, Func<T, E> errorPredicate)
        where E : IError
    {
        if (result.IsFailed) return result;

        if (!await predicate(result.Value))
            return Result.Fail<T>(errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T, E>(this Result<T> result, Func<T, Task<bool>> predicate, Func<T, Task<E>> errorPredicate)
        where E : IError
    {
        if (result.IsFailed) return result;

        if (!await predicate(result.Value))
            return Result.Fail<T>(await errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<bool>> predicate, Func<T, string> errorPredicate)
    {
        if (result.IsFailed) return result;

        if (!await predicate(result.Value))
            return Result.Fail<T>(errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<bool>> predicate, Func<T, Task<string>> errorPredicate)
    {
        if (result.IsFailed) return result;

        if (!await predicate(result.Value))
            return Result.Fail<T>(await errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Result result, Func<Task<bool>> predicate, string errorMessage)
    {
        if (result.IsFailed) return result;

        if (!await predicate())
            return Result.Fail(errorMessage);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure(this Result result, Func<Task<Result>> predicate)
    {
        if (result.IsFailed) return result;

        Result predicateResult = await predicate();
        if (predicateResult.IsFailed)
            return predicateResult;

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<Task<Result>> predicate)
    {
        if (result.IsFailed) return result;

        Result predicateResult = await predicate();
        if (predicateResult.IsFailed)
            return predicateResult.ToResult<T>();

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result> Ensure<T>(this Result result, Func<Task<Result<T>>> predicate)
    {
        if (result.IsFailed) return result;

        Result<T> predicateResult = await predicate();
        if (predicateResult.IsFailed)
            return predicateResult.ToResult();

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<Task<Result<T>>> predicate)
    {
        if (result.IsFailed)
            return result;

        Result<T> predicateResult = await predicate();
        if (predicateResult.IsFailed)
            return predicateResult;

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<Result>> predicate)
    {
        if (result.IsFailed) return result;

        Result predicateResult = await predicate(result.Value);
        if (predicateResult.IsFailed)
            return predicateResult.ToResult<T>();

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<Result<T>>> predicate)
    {
        if (result.IsFailed) return result;

        Result<T> predicateResult = await predicate(result.Value);
        if (predicateResult.IsFailed)
            return predicateResult;

        return result;
    }
}
#pragma warning restore IDE0046 // Convert to conditional expression