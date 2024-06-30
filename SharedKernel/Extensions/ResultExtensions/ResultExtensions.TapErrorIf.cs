using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling</summary>
    public static Task<Result> TapErrorIf(this Result result, bool condition, Func<Task> func) =>
        condition ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapErrorIf(this Result result, bool condition, Func<IEnumerable<IError>, Task> func) =>
        condition ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapErrorIf<T>(this Result<T> result, bool condition, Func<Task> func) =>
        condition ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapErrorIf<T>(this Result<T> result, bool condition, Func<IEnumerable<IError>, Task> func) =>
        condition ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapErrorIf(this Result result, Func<IEnumerable<IError>, bool> predicate, Func<Task> func) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapErrorIf(this Result result, Func<IEnumerable<IError>, bool> predicate, Func<IEnumerable<IError>, Task> func) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapErrorIf<T>(this Result<T> result, Func<IEnumerable<IError>, bool> predicate, Func<Task> func) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapErrorIf<T>(this Result<T> result, Func<IEnumerable<IError>, bool> predicate, Func<IEnumerable<IError>, Task> func) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling.</summary>
    public static Task<Result> TapErrorIf(this Task<Result> resultTask, bool condition, Func<Task> func) =>
        condition ? resultTask.TapError(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapErrorIf(this Task<Result> resultTask, bool condition, Func<IEnumerable<IError>, Task> func) =>
        condition ? resultTask.TapError(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapErrorIf<T>(this Task<Result<T>> resultTask, bool condition, Func<Task> func) =>
        condition ? resultTask.TapError(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapErrorIf<T>(this Task<Result<T>> resultTask, bool condition, Func<IEnumerable<IError>, Task> func) =>
        condition ? resultTask.TapError(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static async Task<Result> TapErrorIf(this Task<Result> resultTask, Func<IEnumerable<IError>, bool> predicate, Func<Task> func)
    {
        Result result = await resultTask;
        return result.IsFailed && predicate(result.Errors)
            ? await result.TapError(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static async Task<Result> TapErrorIf(this Task<Result> resultTask, Func<IEnumerable<IError>, bool> predicate, Func<IEnumerable<IError>, Task> func)
    {
        Result result = await resultTask;
        return result.IsFailed && predicate(result.Errors)
            ? await result.TapError(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapErrorIf<T>(this Task<Result<T>> resultTask, Func<IEnumerable<IError>, bool> predicate, Func<Task> func)
    {
        Result<T> result = await resultTask;
        return result.IsFailed && predicate(result.Errors)
            ? await result.TapError(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapErrorIf<T>(this Task<Result<T>> resultTask, Func<IEnumerable<IError>, bool> predicate, Func<IEnumerable<IError>, Task> func)
    {
        Result<T> result = await resultTask;
        return result.IsFailed && predicate(result.Errors)
            ? await result.TapError(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result TapErrorIf(this Result result, bool condition, Action action) =>
        condition ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result TapErrorIf(this Result result, bool condition, Action<IEnumerable<IError>> action) =>
        condition ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result<T> TapErrorIf<T>(this Result<T> result, bool condition, Action action) =>
        condition ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result<T> TapErrorIf<T>(this Result<T> result, bool condition, Action<IEnumerable<IError>> action) =>
        condition ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result TapErrorIf(this Result result, Func<IEnumerable<IError>, bool> predicate, Action action) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result TapErrorIf(this Result result, Func<IEnumerable<IError>, bool> predicate, Action<IEnumerable<IError>> action) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result<T> TapErrorIf<T>(this Result<T> result, Func<IEnumerable<IError>, bool> predicate, Action action) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(action) : result;

    /// <summary>Executes the given action if the calling result is a failure and condition is true. Returns the calling result.</summary>
    public static Result<T> TapErrorIf<T>(this Result<T> result, Func<IEnumerable<IError>, bool> predicate, Action<IEnumerable<IError>> action) =>
        result.IsFailed && predicate(result.Errors)
            ? result.TapError(action) : result;
}
