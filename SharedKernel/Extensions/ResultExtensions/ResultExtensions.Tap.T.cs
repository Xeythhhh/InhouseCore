using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> resultTask, Action action) =>
        (await resultTask).Tap(action);

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> resultTask, Action<T> action) =>
        (await resultTask).Tap(action);

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> resultTask, Func<Task> func)
    {
        Result<T> result = await resultTask;
        if (result.IsSuccess) await func();
        return result;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> resultTask, Func<T, Task> func)
    {
        Result<T> result = await resultTask;
        if (result.IsSuccess) await func(result.Value);
        return result;
    }
}
