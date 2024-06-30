using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Result;
public partial class Result : ResultBase<Result>
{
    static Result() { }

    public static implicit operator Result(Error error) => Fail(error);
    public static implicit operator Result(List<Error> errors) => Fail(errors);

    /// <summary>Map all errors of the result via errorMapper</summary>
    /// <param name="errorMapper"></param>
    /// <returns></returns>
    public Result MapErrors(Func<IError, IError> errorMapper) =>
        IsSuccess ? this
            : new Result()
                .WithErrors(Errors.Select(errorMapper))
                .WithSuccesses(Successes);

    /// <summary>Map all successes of the result via successMapper</summary>
    /// <param name="successMapper"></param>
    /// <returns></returns>
    public Result MapSuccesses(Func<ISuccess, ISuccess> successMapper) =>
        new Result()
            .WithErrors(Errors)
            .WithSuccesses(Successes.Select(successMapper));

    public Result<TNewValue> ToResult<TNewValue>() =>
        new Result<TNewValue>()
            .WithReasons(Reasons);

    public Result<TNewValue> ToResult<TNewValue>(TNewValue newValue) =>
        ToResult<TNewValue>()
        .WithValue(IsFailed ? default! : newValue);
}