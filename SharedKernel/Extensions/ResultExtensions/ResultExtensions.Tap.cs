using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

/// <summary>Contains extension methods for the Result class</summary>
public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public async static Task<Result> Tap(this Task<Result> resultTask, Action action) =>
        (await resultTask).Tap(action);

    /// <summary>Executes the given action if the calling result is a success. Returns the calling.</summary>
    public static async Task<Result> Tap(this Task<Result> resultTask, Func<Task> func)
    {
        Result result = await resultTask;
        if (result.IsSuccess) await func();
        return result;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static async Task<Result> Tap(this Result result, Func<Task> func)
    {
        if (result.IsSuccess) await func();
        return result;
    }
}