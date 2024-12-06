using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public async Task<Result> TapError(Func<Task> action)
    {
        if (IsFailed) await action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public async Task<Result> TapError(Func<IEnumerable<IError>, Task> action)
    {
        if (IsFailed) await action(Errors);
        return this;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public Result TapError(Action action)
    {
        if (IsFailed) action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a failure. Returns the calling result.</summary>
    public Result TapError(Action<IEnumerable<IError>> action)
    {
        if (IsFailed) action(Errors);
        return this;
    }
}
