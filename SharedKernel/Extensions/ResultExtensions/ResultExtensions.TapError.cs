using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

/// <summary>Contains extension methods for the Result class</summary>
public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result> TapError(this Task<Result> resultTask, Action action) =>
        (await resultTask).TapError(action);

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result> TapError(this Task<Result> resultTask, Action<IEnumerable<IError>> action) =>
        (await resultTask).TapError(action);

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result> TapError(this Task<Result> resultTask, Func<Task> action)
    {
        Result result = await resultTask;
        if (result.IsFailed) await action();
        return result;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result> TapError(this Task<Result> resultTask, Func<IEnumerable<IError>, Task> action)
    {
        Result result = await resultTask;
        if (result.IsFailed) await action(result.Errors);
        return result;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result<TValue>> TapError<TValue>(this Task<Result<TValue>> resultTask, Func<Task> action)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) await action();
        return result;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result<TValue>> TapError<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task> action)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) await action(result.Value);
        return result;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result<TValue>> TapError<TValue>(this Task<Result<TValue>> resultTask,
        Func<IEnumerable<IError>, Task> action)
    {
        Result<TValue> result = await resultTask;
        if (result.IsFailed) await action(result.Errors);
        return result;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result<TValue>> TapError<TValue>(this Task<Result<TValue>> resultTask,
        Action<IEnumerable<IError>> action) =>
        (await resultTask).TapError(action);

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public static async Task<Result<TValue>> TapError<TValue>(this Task<Result<TValue>> resultTask,
        Action action) =>
        (await resultTask).TapError(action);
}