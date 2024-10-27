using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    public static Result<T> OnFailureCompensate<T>(this Result<T> result, Func<Result<T>> func) =>
        result.IsFailed ? func() : result;

    public static Result OnFailureCompensate(this Result result, Func<Result> func) =>
        result.IsFailed ? func() : result;

    public static async Task<Result<T>> OnFailureCompensate<T>(this Result<T> result, Func<Task<Result<T>>> func) =>
        result.IsFailed ? await func() : result;

    public static async Task<Result> OnFailureCompensate(this Result result, Func<Task<Result>> func) =>
        result.IsFailed ? await func() : result;

    public static async Task<Result<T>> OnFailureCompensate<T>(this Task<Result<T>> resultTask, Func<Task<Result<T>>> func)
    {
        Result<T> result = await resultTask;
        return result.IsFailed ? await func() : result;
    }

    public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<Task<Result>> func)
    {
        Result result = await resultTask;
        return result.IsFailed ? await func() : result;
    }

    public static async Task<Result<T>> OnFailureCompensate<T>(this Task<Result<T>> resultTask, Func<Result<T>> func)
    {
        Result<T> result = await resultTask;
        return result.OnFailureCompensate(func);
    }

    public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<Result> func)
    {
        Result result = await resultTask;
        return result.OnFailureCompensate(func);
    }

    public static Result<T> OnFailureCompensate<T>(this Result<T> result, Func<IEnumerable<IError>, Result<T>> func) =>
        result.IsFailed ? func(result.Errors) : result;

    public static Result OnFailureCompensate(this Result result, Func<IEnumerable<IError>, Result> func) =>
        result.IsFailed ? func(result.Errors) : result;

    public static async Task<Result<T>> OnFailureCompensate<T>(this Task<Result<T>> resultTask, Func<IEnumerable<IError>, Task<Result<T>>> func)
    {
        Result<T> result = await resultTask;
        return result.IsFailed ? await func(result.Errors) : result;
    }

    public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<IEnumerable<IError>, Task<Result>> func)
    {
        Result result = await resultTask;
        return result.IsFailed ? await func(result.Errors) : result;
    }

    public static async Task<Result<T>> OnFailureCompensate<T>(this Result<T> result, Func<IEnumerable<IError>, Task<Result<T>>> func) =>
        result.IsFailed ? await func(result.Errors) : result;

    public static async Task<Result> OnFailureCompensate(this Result result, Func<IEnumerable<IError>, Task<Result>> func) =>
        result.IsFailed ? await func(result.Errors) : result;

    public static async Task<Result<T>> OnFailureCompensate<T>(this Task<Result<T>> resultTask, Func<IEnumerable<IError>, Result<T>> func)
    {
        Result<T> result = await resultTask;
        return result.OnFailureCompensate(func);
    }

    public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<IEnumerable<IError>, Result> func)
    {
        Result result = await resultTask;
        return result.OnFailureCompensate(func);
    }
}