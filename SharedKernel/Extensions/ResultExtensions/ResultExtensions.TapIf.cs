using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

/// <summary>Contains extension methods for the Result class</summary>
public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Result TapIf(this Result result, bool condition, Action action) =>
        condition ? result.Tap(action) : result;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Result<T> TapIf<T>(this Result<T> result, bool condition, Action action) =>
        condition ? result.Tap(action) : result;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Result<T> TapIf<T>(this Result<T> result, bool condition, Action<T> action) =>
        condition ? result.Tap(action) : result;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Result<T> TapIf<T>(this Result<T> result, Func<T, bool> predicate, Action action) =>
        result.IsSuccess && predicate(result.Value)
            ? result.Tap(action) : result;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Result<T> TapIf<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action) =>
        result.IsSuccess && predicate(result.Value)
            ? result.Tap(action) : result;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapIf(this Task<Result> resultTask, bool condition, Action action) =>
        condition ? resultTask.Tap(action) : resultTask;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, bool condition, Action action) =>
        condition ? resultTask.Tap(action) : resultTask;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, bool condition, Action<T> action) =>
        condition ? resultTask.Tap(action) : resultTask;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapIf(this Result result, bool condition, Func<Task> func) =>
        condition ? result.Tap(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Result<T> result, bool condition, Func<Task> func) =>
        condition ? result.Tap(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Result<T> result, bool condition, Func<T, Task> func) =>
        condition ? result.Tap(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Result<T> result, Func<T, bool> predicate, Func<Task> func) =>
        result.IsSuccess && predicate(result.Value)
            ? result.Tap(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Result<T> result, Func<T, bool> predicate, Func<T, Task> func) =>
        result.IsSuccess && predicate(result.Value)
            ? result.Tap(func) : Task.FromResult(result);

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result> TapIf(this Task<Result> resultTask, bool condition, Func<Task> func) =>
        condition ? resultTask.Tap(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, bool condition, Func<Task> func) =>
        condition ? resultTask.Tap(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, bool condition, Func<T, Task> func) =>
        condition ? resultTask.Tap(func) : resultTask;

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<Task> func)
    {
        Result<T> result = await resultTask;
        return result.IsSuccess && predicate(result.Value)
            ? await result.Tap(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Task> func)
    {
        Result<T> result = await resultTask;
        return result.IsSuccess && predicate(result.Value)
            ? await result.Tap(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<Task<bool>> predicate, Func<T, Task> func)
    {
        Result<T> result = await resultTask;
        return result.IsSuccess && await predicate()
            ? await result.Tap(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, Func<T, Task> func)
    {
        Result<T> result = await resultTask;
        return result.IsSuccess && await predicate(result.Value)
            ? await result.Tap(func) : result;
    }

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result> TapIf(this Task<Result> resultTask, Func<bool> predicate, Action action)
    {
        Result result = await resultTask;
        return result.IsSuccess && predicate()
            ? result.Tap(action) : result;
    }

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Action action)
    {
        Result<T> result = await resultTask;
        return result.IsSuccess && predicate(result.Value)
            ? result.Tap(action) : result;
    }

    /// <summary>Executes the given action if the calling result is a success and condition is true. Returns the calling result.</summary>
    public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Action<T> action)
    {
        Result<T> result = await resultTask;
        return result.IsSuccess && predicate(result.Value)
            ? result.Tap(action) : result;
    }
}