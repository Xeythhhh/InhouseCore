using FluentResults;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    public static async Task<Result> OnSuccessTry(this Result result, Func<Task> func, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result : await Result.Try(func, errorHandler);

    public static async Task<Result> OnSuccessTry<T>(this Result<T> result, Func<T, Task> func, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result.ToResult() : await Result.Try(() => func.Invoke(result.Value), errorHandler);

    public static async Task<Result> OnSuccessTry(this Task<Result> task, Action action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public static Result OnSuccessTry(this Result result, Action action, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result : Result.Try(action, errorHandler)
            .WithReasons(result.Reasons);

    public static async Task<Result<T>> OnSuccessTry<T>(this Task<Result<T>> task, Action action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public static Result<T> OnSuccessTry<T>(this Result<T> result, Action action, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result : Result.Try(action, errorHandler)
            .ToResult<T>().WithValue(result.Value)
            .WithReasons(result.Reasons);

    public static async Task<Result<T>> OnSuccessTry<T>(this Task<Result<T>> task, Func<T> action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    private static Result<T> OnSuccessTry<T>(this Result<T> result, Func<T> action, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result : Result.Try(action, errorHandler)
            .WithReasons(result.Reasons);

    public async static Task<Result<T>> OnSuccessTry<T>(this Task<Result<T>> task, Func<T, T> action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public static Result<T> OnSuccessTry<T>(this Result<T> result, Func<T, T> action, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result : Result.Try(() => action.Invoke(result.Value), errorHandler)
            .WithValue(result.Value)
            .WithReasons(result.Reasons);

    public async static Task<Result<T>> OnSuccessTry<T>(this Task<Result<T>> task, Action<T> action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public static Result<T> OnSuccessTry<T>(this Result<T> result, Action<T> action, Func<Exception, IError> errorHandler = null!) =>
        result.IsFailed ? result : Result.Try(() => action.Invoke(result.Value), errorHandler)
            .ToResult(result.Value)
            .WithReasons(result.Reasons);
}