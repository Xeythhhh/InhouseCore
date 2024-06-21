using FluentResults;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static Result OnSuccess(this Result result, Action action)
    {
        if (result.IsSuccess) action();
        return result;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static Result<T> OnSuccess<T>(this Result<T> result, Action action)
    {
        if (result.IsSuccess) action();
        return result;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess) action(result.Value);
        return result;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static Result<TResult> OnSuccess<TResult, TParam>(this Result<TResult> result, Action<TParam> action, TParam param)
    {
        if (result.IsSuccess) action(param);
        return result;
    }

    /// <summary>Executes the given action if the calling result is a success. Returns the calling result.</summary>
    public static Result OnSuccess<TParam>(this Result result, Action<TParam> action, TParam param)
    {
        if (result.IsSuccess) action(param);
        return result;
    }
}
