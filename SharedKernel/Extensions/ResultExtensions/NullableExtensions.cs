using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static class NullableExtensions
{
    private static readonly Error NullError = new("Value was 'null' when converting to result.");

    public static Result<T> ToResult<T>(this T? nullable, IError? error = null)
        where T : struct =>
        !nullable.HasValue
            ? Result.Fail<T>(error ?? NullError)
            : Result.Ok(nullable.Value);

    public static Result<T> ToResult<T>(this T? obj, IError? error = null)
        where T : class =>
        obj == null
            ? Result.Fail<T>(error ?? NullError)
            : Result.Ok(obj);

    public static Result ToResultWithoutValue<T>(this T? obj, IError? error = null)
        where T : class =>
        obj == null
            ? Result.Fail(error ?? NullError)
            : Result.Ok();

    public static async Task<Result<T>> ToResultAsync<T>(this Task<T?> nullableTask, IError? error = null)
        where T : struct =>
        (await nullableTask.ConfigureAwait(false))
        .ToResult(error);

    public static async Task<Result<T>> ToResultAsync<T>(this Task<T?> nullableTask, IError? error = null)
    where T : class =>
        (await nullableTask.ConfigureAwait(false))
        .ToResult(error);

    public static async Task<Result> ToResultWithoutValueAsync<T>(this Task<T?> nullableTask, IError? error = null)
    where T : class =>
        (await nullableTask.ConfigureAwait(false))
        .ToResultWithoutValue(error);

    public static async Task<Result> ToResultWithoutValueAsync<T>(this Task<Result<T?>> nullableTask, IError? error = null)
    where T : class =>
        (await nullableTask.ConfigureAwait(false))
        .ToResultWithoutValue(error);

    public static async ValueTask<Result<T>> ToResultAsync<T>(this ValueTask<T?> nullableTask, IError? error = null)
        where T : struct =>
        (await nullableTask.ConfigureAwait(false))
        .ToResult(error);
}