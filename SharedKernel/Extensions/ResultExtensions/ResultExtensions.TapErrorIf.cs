using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the result is a failure and the condition is true. Returns the original result.</summary>
    public static Task<Result> TapErrorIf(this Result result, bool condition, Func<IEnumerable<IError>, Task> func) =>
        condition && result.IsFailed ? result.TapError(func) : Task.FromResult(result);

    public static Task<Result<T>> TapErrorIf<T>(this Result<T> result, bool condition, Func<IEnumerable<IError>, Task> func) =>
        condition && result.IsFailed ? result.TapError(func) : Task.FromResult(result);

    public static async Task<Result> TapErrorIf(this Task<Result> resultTask, bool condition, Func<IEnumerable<IError>, Task> func)
    {
        var result = await resultTask.ConfigureAwait(false);
        return condition && result.IsFailed ? await result.TapError(func) : result;
    }

    public static async Task<Result<T>> TapErrorIf<T>(this Task<Result<T>> resultTask, bool condition, Func<IEnumerable<IError>, Task> func)
    {
        var result = await resultTask.ConfigureAwait(false);
        return condition && result.IsFailed ? await result.TapError(func) : result;
    }

    public static async Task<Result> TapErrorIf(this Task<Result> resultTask, Func<IEnumerable<IError>, bool> predicate, Func<IEnumerable<IError>, Task> func)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsFailed && predicate(result.Errors) ? await result.TapError(func) : result;
    }

    public static async Task<Result<T>> TapErrorIf<T>(this Task<Result<T>> resultTask, Func<IEnumerable<IError>, bool> predicate, Func<IEnumerable<IError>, Task> func)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsFailed && predicate(result.Errors) ? await result.TapError(func) : result;
    }

    public static Result TapErrorIf(this Result result, bool condition, Action<IEnumerable<IError>> action) =>
        condition && result.IsFailed ? result.TapError(action) : result;

    public static Result<T> TapErrorIf<T>(this Result<T> result, bool condition, Action<IEnumerable<IError>> action) =>
        condition && result.IsFailed ? result.TapError(action) : result;

    public static Result TapErrorIf(this Result result, Func<IEnumerable<IError>, bool> predicate, Action<IEnumerable<IError>> action) =>
        result.IsFailed && predicate(result.Errors) ? result.TapError(action) : result;

    public static Result<T> TapErrorIf<T>(this Result<T> result, Func<IEnumerable<IError>, bool> predicate, Action<IEnumerable<IError>> action) =>
        result.IsFailed && predicate(result.Errors) ? result.TapError(action) : result;

    // Overloads for accessing failure values
    public static Result<T> TapErrorIf<T>(this Result<T> result, Func<T, bool> failureCondition, Action<IEnumerable<IError>> action) =>
        result.IsFailed && failureCondition(result.Value) ? result.OnFailure(action) : result;
}
