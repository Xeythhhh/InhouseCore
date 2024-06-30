namespace SharedKernel.Primitives.Result;
public partial class Result<TValue>
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public async Task<Result<TValue>> Tap(Func<Task> func)
    {
        if (IsSuccess) await func();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public async Task<Result<TValue>> Tap(Func<TValue, Task> func)
    {
        if (IsSuccess) await func(Value);
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public Result<TValue> Tap(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public Result<TValue> Tap(Action<TValue> action)
    {
        if (IsSuccess) action(Value);
        return this;
    }
}