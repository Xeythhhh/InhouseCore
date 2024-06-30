using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    public static async Task<Result<TValue>> OnSuccessTry<TValue>(this Task<Result<TValue>> task,
        Action action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public async static Task<Result<TValue>> OnSuccessTry<TValue>(this Task<Result<TValue>> task,
        Action<TValue> action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public static async Task<Result<TValue>> OnSuccessTry<TValue>(this Task<Result<TValue>> task,
        Func<TValue> action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);

    public async static Task<Result<TValue>> OnSuccessTry<TValue>(this Task<Result<TValue>> task,
        Func<TValue, TValue> action, Func<Exception, IError> errorHandler = null!) =>
        (await task).OnSuccessTry(action, errorHandler);
}