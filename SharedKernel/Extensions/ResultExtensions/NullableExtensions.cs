using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static class NullableExtensions
{
    private static readonly Error NullStructError = new("Nullable struct was 'null' when converting to result.");
    private static readonly Error NullReferenceError = new("Reference type was 'null' when converting to result.");

    /// <summary>Converts a nullable struct to a <see cref="Result{T}"/>. Returns a failure if the value is null.</summary>
    public static Result<T> ToResult<T>(this T? nullable, IError? error = null) where T : struct
        => nullable.HasValue ? Result.Ok(nullable.Value) : Result.Fail<T>(error ?? NullStructError);

    /// <summary>Converts a nullable reference type to a <see cref="Result{T}"/>. Returns a failure if the value is null.</summary>
    public static Result<T> ToResult<T>(this T? obj, IError? error = null) where T : class
        => obj != null ? Result.Ok(obj) : Result.Fail<T>(error ?? NullReferenceError);

    /// <summary>Converts a task returning a nullable struct to a <see cref="Result{T}"/>. Returns a failure if the value is null.</summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<T?> nullableTask, IError? error = null) where T : struct
    {
        T? result = await nullableTask.ConfigureAwait(false);
        return result.ToResult(error);
    }

    /// <summary>Converts a task returning a nullable reference type to a <see cref="Result{T}"/>. Returns a failure if the value is null.</summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<T?> nullableTask, IError? error = null) where T : class
    {
        T? result = await nullableTask.ConfigureAwait(false);
        return result.ToResult(error);
    }

    /// <summary>Converts a <see cref="ValueTask"/> returning a nullable struct to a <see cref="Result{T}"/>. Returns a failure if the value is null.</summary>
    public static async ValueTask<Result<T>> ToResultAsync<T>(this ValueTask<T?> nullableTask, IError? error = null) where T : struct
    {
        T? result = await nullableTask.ConfigureAwait(false);
        return result.ToResult(error);
    }

    /// <summary>Converts a <see cref="ValueTask"/> returning a nullable reference type to a <see cref="Result{T}"/>. Returns a failure if the value is null.</summary>
    public static async ValueTask<Result<T>> ToResultAsync<T>(this ValueTask<T?> nullableTask, IError? error = null) where T : class
    {
        T? result = await nullableTask.ConfigureAwait(false);
        return result.ToResult(error);
    }
}
