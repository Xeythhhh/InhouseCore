using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result<TValue>
{
    public Result<TValue> OnSuccessTry(Func<TValue> action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(action, errorHandler)
                .WithReasons(Reasons);

    public async Task<Result> OnSuccessTry(Func<TValue, Task> func, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? ToResult()
            : await Result.Try(() => func.Invoke(Value), errorHandler);

    public Result<TValue> OnSuccessTry(Func<TValue, TValue> action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(() => action.Invoke(Value), errorHandler)
                .WithValue(Value)
                .WithReasons(Reasons);

    public Result<TValue> OnSuccessTry(Action action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(action, errorHandler)
                .ToResult<TValue>().WithValue(Value)
                .WithReasons(Reasons);

    public Result<TValue> OnSuccessTry(Action<TValue> action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(() => action.Invoke(Value), errorHandler)
                .ToResult(Value)
                .WithReasons(Reasons);
}
