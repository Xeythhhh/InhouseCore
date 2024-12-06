using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Converts an asynchronous operation to a <see cref="Result{T}"/> containing a given value if successful.</summary>
    public static async Task<Result<T>> ToResult<T>(this Task<Result> resultTask, T value)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.ToResult(value);
    }

    /// <summary>Converts an asynchronous operation to a <see cref="Result{T}"/> containing a given value if successful.</summary>
    public static async ValueTask<Result<T>> ToResult<T>(this ValueTask<Result> resultTask, T value)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.ToResult(value);
    }
}
