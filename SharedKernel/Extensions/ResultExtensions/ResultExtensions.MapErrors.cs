using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>Maps the errors of an asynchronous operation using a given error-mapping function.</summary>
    public static async Task<Result> MapErrors(this Task<Result> resultTask, Func<IError, IError> errorMapper)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.MapErrors(errorMapper);
    }

    /// <summary>Maps the errors of an asynchronous operation using a given error-mapping function.</summary>
    public static async ValueTask<Result> MapErrors(this ValueTask<Result> resultTask, Func<IError, IError> errorMapper)
    {
        Result result = await resultTask.ConfigureAwait(false);
        return result.MapErrors(errorMapper);
    }
}
