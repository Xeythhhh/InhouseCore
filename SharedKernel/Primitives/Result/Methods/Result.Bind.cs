namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Convert result to result with value that may fail.</summary>
    /// <param name="bind">Transformation that may fail.</param>
    public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> bind) =>
        IsSuccess ? bind().WithReasons(Reasons)
            : ToResult<TNewValue>(default!);

    /// <summary>Convert result to result with value that may fail asynchronously.</summary>
    /// <param name="bind">Transformation that may fail.</param>
    public async Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> bind) =>
        IsSuccess ? (await bind()).WithReasons(Reasons)
            : ToResult<TNewValue>(default!);

    /// <summary>Execute an action which returns a <see cref="Result"/>.</summary>
    /// <param name="bind">Action that may fail.</param>
    public Result Bind(Func<Result> bind) =>
        IsSuccess ? bind().WithReasons(Reasons) : this;

    /// <summary>Execute an action which returns a <see cref="Result"/> asynchronously.</summary>
    /// <param name="bind">Action that may fail.</param>
    public async Task<Result> Bind(Func<Task<Result>> bind) =>
        IsSuccess ? (await bind()).WithReasons(Reasons) : this;
}