namespace SharedKernel.Primitives.Result;
public partial class Result<T>
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public async Task<Result<T>> Tap(Func<Task> func)
    {
        if (IsSuccess) await func();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public async Task<Result<T>> Tap(Func<T, Task> func)
    {
        if (IsSuccess) await func(Value);
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public Result<T> Tap(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling </summary>
    public Result<T> Tap(Action<T> action)
    {
        if (IsSuccess) action(Value);
        return this;
    }
}