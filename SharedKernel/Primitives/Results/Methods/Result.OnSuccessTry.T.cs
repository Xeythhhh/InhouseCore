using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result<T>
{
    public Result<T> OnSuccessTry(Func<T> action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(action, errorHandler)
                .WithReasons(Reasons);

    public async Task<Result> OnSuccessTry(Func<T, Task> func, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? ToResult()
            : await Result.Try(() => func.Invoke(Value), errorHandler);

    public Result<T> OnSuccessTry(Func<T, T> action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(() => action.Invoke(Value), errorHandler)
                .WithValue(Value)
                .WithReasons(Reasons);

    public Result<T> OnSuccessTry(Action action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(action, errorHandler)
                .ToResult<T>().WithValue(Value)
                .WithReasons(Reasons);

    public Result<T> OnSuccessTry(Action<T> action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Result.Try(() => action.Invoke(Value), errorHandler)
                .ToResult(Value)
                .WithReasons(Reasons);
}
