namespace SharedKernel.Primitives.Result;

public partial class Result<TValue>
{
    /// <summary>Convert result with value to result with another value that may fail</summary>
    /// <param name="bind">Transformation that may fail.</param>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> bind) =>
        IsSuccess ? bind(Value).WithReasons(Reasons) : ToResult<TNewValue>();

    /// <summary>Convert result with value to result with another value that may fail</summary>
    /// <param name="bind">Transformation that may fail.</param>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, TNewValue> bind) =>
        IsSuccess
            ? Result.Try(() => bind(Value))
                .WithReasons(Reasons)
            : ToResult<TNewValue>();

    /// <summary>Convert result with value to result with another value that may fail asynchronously</summary>
    /// <param name="bind">Transformation that may fail.</param>
    public async Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> bind) =>
        IsSuccess ? (await bind(Value)).WithReasons(Reasons) : ToResult<TNewValue>();

    /// <summary>Execute an action which returns a <see cref="Result"/>.</summary>
    /// <param name="action">Action that may fail.</param>
    public Result Bind(Func<TValue, Result> action) =>
        IsSuccess ? action(Value).WithReasons(Reasons) : ToResult();

    /// <summary>Execute an action which returns a <see cref="Result"/> asynchronously</summary>
    /// <param name="action">Action that may fail.</param>
    public async Task<Result> Bind(Func<TValue, Task<Result>> action) =>
        IsSuccess ? (await action(Value)).WithReasons(Reasons) : ToResult();
}
