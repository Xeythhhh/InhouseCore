namespace SharedKernel.Primitives.Result;
public partial class Result<T>
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result<T> OnSuccess(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result<T> OnSuccess(Action<T> action)
    {
        if (IsSuccess) action(Value);
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result<T> OnSuccess<TParam>(Action<TParam> action, TParam param)
    {
        if (IsSuccess) action(param);
        return this;
    }
}
