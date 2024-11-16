using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{

    public static Result ToResultWithoutValue<T>(this Result<T> resultWithValue, IError? error = null) =>
        (!resultWithValue.IsFailed
            ? Result.Fail(error ?? new Error("Result has status failed."))
            : Result.Ok())
        .WithReasons(resultWithValue.Reasons);

    public static async Task<Result> ToResultWithoutValueAsync<T>(this Task<Result<T>> task, IError? error = null) =>
        (await task.ConfigureAwait(false))
        .ToResultWithoutValue(error);
}