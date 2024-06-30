using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    public static async Task<Result> OnSuccessTry(this Task<Result> task, Action action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);
}