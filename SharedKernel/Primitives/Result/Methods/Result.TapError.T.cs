using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result<TValue>
{
    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public async Task<Result<TValue>> TapError(Func<Task> func)
    {
        if (IsFailed) await func();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public async Task<Result<TValue>> TapError(Func<IEnumerable<IError>, Task> func)
    {
        if (IsFailed) await func(Errors);
        return this;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public Result<TValue> TapError(Action action)
    {
        if (IsFailed) action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public Result<TValue> TapError(Action<IEnumerable<IError>> action)
    {
        if (IsFailed) action(Errors);
        return this;
    }
}