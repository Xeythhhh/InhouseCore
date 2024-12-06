using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Executes the provided action if the <see cref="Result"/> is a failure.</summary>
    public static Result OnFailure(this Result result, Action<IEnumerable<IError>> action)
    {
        if (result.IsFailed) action(result.Errors);
        return result;
    }

    /// <summary>Executes the provided action if the <see cref="Result{T}"/> is a failure.</summary>
    public static Result<T> OnFailure<T>(this Result<T> result, Action<IEnumerable<IError>> action)
    {
        if (result.IsFailed) action(result.Errors);
        return result;
    }

    /// <summary>Executes the provided asynchronous function if the <see cref="Result"/> is a failure.</summary>
    public static async Task<Result> OnFailure(this Task<Result> resultTask, Func<IEnumerable<IError>, Task> func)
    {
        Result result = await resultTask.ConfigureAwait(false);
        if (result.IsFailed) await func(result.Errors).ConfigureAwait(false);
        return result;
    }

    /// <summary>Executes the provided asynchronous function if the <see cref="Result{T}"/> is a failure.</summary>
    public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> resultTask, Func<IEnumerable<IError>, Task> func)
    {
        Result<T> result = await resultTask.ConfigureAwait(false);
        if (result.IsFailed) await func(result.Errors).ConfigureAwait(false);
        return result;
    }

    /// <summary>Executes the provided action if the <see cref="Result"/> is a failure.</summary>
    public static Task<Result> OnFailure(this Task<Result> resultTask, Action<IEnumerable<IError>> action)
        => resultTask.ContinueWith(task =>
        {
            Result result = task.Result;
            if (result.IsFailed) action(result.Errors);
            return result;
        }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

    /// <summary>Executes the provided action if the <see cref="Result{T}"/> is a failure.</summary>
    public static Task<Result<T>> OnFailure<T>(this Task<Result<T>> resultTask, Action<IEnumerable<IError>> action)
        => resultTask.ContinueWith(task =>
        {
            Result<T> result = task.Result;
            if (result.IsFailed) action(result.Errors);
            return result;
        }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

    /// <summary>Executes the provided asynchronous function if the <see cref="Result"/> is a failure.</summary>
    public static async ValueTask<Result> OnFailure(this ValueTask<Result> resultTask, Func<IEnumerable<IError>, Task> func)
    {
        Result result = await resultTask.ConfigureAwait(false);
        if (result.IsFailed) await func(result.Errors).ConfigureAwait(false);
        return result;
    }

    /// <summary>Executes the provided asynchronous function if the <see cref="Result{T}"/> is a failure.</summary>
    public static async ValueTask<Result<T>> OnFailure<T>(this ValueTask<Result<T>> resultTask, Func<IEnumerable<IError>, Task> func)
    {
        Result<T> result = await resultTask.ConfigureAwait(false);
        if (result.IsFailed) await func(result.Errors).ConfigureAwait(false);
        return result;
    }

    /// <summary>Executes the provided action if the <see cref="Result"/> is a failure.</summary>
    public static ValueTask<Result> OnFailure(this ValueTask<Result> resultTask, Action<IEnumerable<IError>> action)
        => new(resultTask.AsTask().ContinueWith(task =>
        {
            Result result = task.Result;
            if (result.IsFailed) action(result.Errors);
            return result;
        }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously));

    /// <summary>Executes the provided action if the <see cref="Result{T}"/> is a failure.</summary>
    public static ValueTask<Result<T>> OnFailure<T>(this ValueTask<Result<T>> resultTask, Action<IEnumerable<IError>> action)
        => new(resultTask.AsTask().ContinueWith(task =>
        {
            Result<T> result = task.Result;
            if (result.IsFailed) action(result.Errors);
            return result;
        }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously));
}
