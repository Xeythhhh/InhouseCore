namespace SharedKernel.Primitives.Result;
public partial class Result<TValue>
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result<TValue> OnSuccess(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result<TValue> OnSuccess(Action<TValue> action)
    {
        if (IsSuccess) action(Value);
        return this;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public Result<TValue> OnSuccess<TParam>(Action<TParam> action, TParam param)
    {
        if (IsSuccess) action(param);
        return this;
    }
}
