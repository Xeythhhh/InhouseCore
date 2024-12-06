using System.Runtime.CompilerServices;

namespace SharedKernel.Primitives.Result;

public partial class Result
{
    /// <summary>Converts a result to another result with a value that may fail.</summary>
    public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsSuccess ? bind().WithReasons(Reasons) : ToResult<TNewValue>(default!);
    }

    /// <summary>Converts a result to another result with a value that may fail asynchronously.</summary>
    public async Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsSuccess ? (await bind()).WithReasons(Reasons) : ToResult<TNewValue>(default!);
    }

    /// <summary>Executes an action that returns a <see cref="Result"/>.</summary>
    public Result Bind(Func<Result> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsSuccess ? bind().WithReasons(Reasons) : this;
    }

    /// <summary>Executes an asynchronous action that returns a <see cref="Result"/>.</summary>
    public async Task<Result> Bind(Func<Task<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsSuccess ? (await bind()).WithReasons(Reasons) : this;
    }

    /// <summary>Converts a result to another result with a value that may fail using <see cref="ValueTask"/>.</summary>
    [OverloadResolutionPriority(1)]
    public async ValueTask<Result<TNewValue>> Bind<TNewValue>(Func<ValueTask<Result<TNewValue>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsSuccess ? (await bind()).WithReasons(Reasons) : ToResult<TNewValue>(default!);
    }

    /// <summary>Executes an asynchronous action that returns a <see cref="Result"/> using <see cref="ValueTask"/>.</summary>
    [OverloadResolutionPriority(1)]
    public async ValueTask<Result> Bind(Func<ValueTask<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsSuccess ? (await bind()).WithReasons(Reasons) : this;
    }
}
