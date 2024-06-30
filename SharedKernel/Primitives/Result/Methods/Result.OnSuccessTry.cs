using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    public async Task<Result> OnSuccessTry(Func<Task> func, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : await Try(func, errorHandler);

    public Result OnSuccessTry(Action action, Func<Exception, IError> errorHandler = null!) =>
        IsFailed ? this
            : Try(action, errorHandler)
                .WithReasons(Reasons);
}
