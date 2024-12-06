using System.Runtime.CompilerServices;

namespace SharedKernel.Primitives.Result;

public partial class Result<T>
{
    /// <summary>Binds the result to another result via an asynchronous function returning a <see cref="ValueTask{TResult}"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the bind function is null.</exception>
    [OverloadResolutionPriority(1)]
    public ValueTask<Result<TNew>> Bind<TNew>(Func<T, ValueTask<Result<TNew>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsFailed
            ? new ValueTask<Result<TNew>>(ToResult<TNew>())
            : new ValueTask<Result<TNew>>(BindAsyncInternal(bind));
    }

    /// <summary>Binds the result to another result via an asynchronous function returning a <see cref="Task{TResult}"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the bind function is null.</exception>
    public async Task<Result<TNew>> Bind<TNew>(Func<T, Task<Result<TNew>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsFailed
            ? ToResult<TNew>()
            : await bind(Value).ConfigureAwait(false);
    }

    /// <summary>Binds the result to another result via a synchronous function.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the bind function is null.</exception>
    public Result<TNew> Bind<TNew>(Func<T, Result<TNew>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsFailed
            ? ToResult<TNew>()
            : bind(Value);
    }

    /// <summary>Binds the result to another result without returning a value via a synchronous function.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the bind function is null.</exception>
    public Result Bind(Func<T, Result> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsFailed
            ? new Result().WithReasons(Reasons)
            : bind(Value).WithReasons(Reasons);
    }

    /// <summary>Binds the result to another result without returning a value via an asynchronous function returning a <see cref="Task"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the bind function is null.</exception>
    public async Task<Result> Bind(Func<T, Task<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsFailed
            ? new Result().WithReasons(Reasons)
            : await bind(Value).ConfigureAwait(false);
    }

    /// <summary>Binds the result to another result without returning a value via an asynchronous function returning a <see cref="ValueTask"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the bind function is null.</exception>
    [OverloadResolutionPriority(1)]
    public async ValueTask<Result> Bind(Func<T, ValueTask<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return IsFailed
            ? new Result().WithReasons(Reasons)
            : await bind(Value).ConfigureAwait(false);
    }

    /// <summary>Internal helper method for handling asynchronous ValueTask binding logic.</summary>
    private async Task<Result<TNew>> BindAsyncInternal<TNew>(Func<T, ValueTask<Result<TNew>>> bind) =>
        await bind(Value).ConfigureAwait(false);
}
