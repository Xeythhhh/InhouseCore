namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public async Task<Result> Tap(Func<Task> func)
    {
        if (IsSuccess) await func();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result Tap(Action action)
    {
        if (IsSuccess) action();
        return this;
    }
}