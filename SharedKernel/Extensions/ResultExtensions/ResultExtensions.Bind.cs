using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    /// <summary>Binds the result of an asynchronous operation to another asynchronous result.</summary>
    public static async Task<Result<TNew>> Bind<TOld, TNew>(this Task<Result<TOld>> resultTask, Func<TOld, Task<Result<TNew>>> bind)
    {
        Result<TOld> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind(result.Value).ConfigureAwait(false) : Result.Fail<TNew>(result.Errors);
    }

    /// <summary>Binds the result of an asynchronous operation to another asynchronous result.</summary>
    public static async ValueTask<Result<TNew>> Bind<TOld, TNew>(this ValueTask<Result<TOld>> resultTask, Func<TOld, ValueTask<Result<TNew>>> bind)
    {
        Result<TOld> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind(result.Value).ConfigureAwait(false) : Result.Fail<TNew>(result.Errors);
    }

    /// <summary>Binds the result of an asynchronous operation to a synchronous result.</summary>
    public static async Task<Result<TNew>> Bind<TOld, TNew>(this Task<Result<TOld>> resultTask, Func<TOld, Result<TNew>> bind)
    {
        Result<TOld> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? bind(result.Value) : Result.Fail<TNew>(result.Errors);
    }

    /// <summary>Binds the result of an asynchronous operation to a synchronous result.</summary>
    public static async ValueTask<Result<TNew>> Bind<TOld, TNew>(this ValueTask<Result<TOld>> resultTask, Func<TOld, Result<TNew>> bind)
    {
        Result<TOld> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? bind(result.Value) : Result.Fail<TNew>(result.Errors);
    }

    /// <summary>Binds the result of an asynchronous operation to another result without returning a value.</summary>
    public static async Task<Result> Bind<T>(this Task<Result<T>> resultTask, Func<T, Result> bind)
    {
        Result<T> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? bind(result.Value) : Result.Fail(result.Errors);
    }

    /// <summary>Binds the result of an asynchronous operation to another asynchronous result without returning a value.</summary>
    public static async Task<Result> Bind<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> bind)
    {
        Result<T> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind(result.Value).ConfigureAwait(false) : Result.Fail(result.Errors);
    }

    /// <summary>Binds the result of an asynchronous operation to another asynchronous result without returning a value.</summary>
    public static async ValueTask<Result> Bind<T>(this ValueTask<Result<T>> resultTask, Func<T, ValueTask<Result>> bind)
    {
        Result<T> result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind(result.Value).ConfigureAwait(false) : Result.Fail(result.Errors);
    }

    /// <summary>Binds an asynchronous operation to another asynchronous result.</summary>
    public static async Task<Result<TNew>> Bind<TNew>(this Task<Result> resultTask, Func<Task<Result<TNew>>> bind)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind().ConfigureAwait(false) : Result.Fail<TNew>(result.Errors);
    }

    /// <summary>Binds an asynchronous operation to another asynchronous result.</summary>
    public static async Task<Result> Bind(this Task<Result> resultTask, Func<Task<Result>> bind)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind().ConfigureAwait(false) : Result.Fail(result.Errors);
    }

    /// <summary>Binds an asynchronous operation to another asynchronous result.</summary>
    public static async ValueTask<Result<TNew>> Bind<TNew>(this ValueTask<Result> resultTask, Func<ValueTask<Result<TNew>>> bind)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess ? await bind().ConfigureAwait(false) : Result.Fail<TNew>(result.Errors);
    }
}
