using FluentResults;

using Error = FluentResults.Error;
#pragma warning disable IDE0046 // Convert to conditional expression

namespace SharedKernel.Extensions.ResultExtensions;

/// <summary>Contains extension methods for the Result class</summary>
public static partial class ResultExtensions
{
    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T, E>(this Result<T> result, Func<T, bool> predicate, Func<T, E> errorPredicate)
        where E : IError
    {
        if (result.IsFailed) return result;

        if (!predicate(result.Value))
            return Result.Fail<T>(errorPredicate(result.Value));

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T, E>(this Result<T> result, Func<T, bool> predicate, E error)
        where E : IError =>
        result.Ensure(predicate, _ => error);

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        => result.Ensure(predicate, new Error(errorMessage));

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Func<T, string> errorPredicate)
        => result.Ensure(predicate, _ => new Error(errorPredicate(result.Value)));

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T>(this Result<T> result, Func<Result> predicate)
    {
        if (result.IsFailed) return result;

        Result predicateResult = predicate();

        if (predicateResult.IsFailed)
            return Result.Fail<T>(predicateResult.Errors);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T>(this Result<T> result, Func<Result<T>> predicate)
    {
        if (result.IsFailed) return result;

        Result<T> predicateResult = predicate();

        if (predicateResult.IsFailed)
            return Result.Fail<T>(predicateResult.Errors);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, Result> predicate)
    {
        if (result.IsFailed) return result;

        Result predicateResult = predicate(result.Value);

        if (predicateResult.IsFailed)
            return Result.Fail<T>(predicateResult.Errors);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, Result<T>> predicate)
    {
        if (result.IsFailed) return result;

        Result<T> predicateResult = predicate(result.Value);

        if (predicateResult.IsFailed)
            return Result.Fail<T>(predicateResult.Errors);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is false. Otherwise returns the starting result.</summary>
    public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
    {
        if (result.IsFailed) return result;

        if (!predicate())
            return Result.Fail(errorMessage);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static Result Ensure(this Result result, Func<Result> predicate)
    {
        if (result.IsFailed) return result;

        Result predicateResult = predicate();

        if (predicateResult.IsFailed)
            return Result.Fail(predicateResult.Errors);

        return result;
    }

    /// <summary>Returns a new failure result if the predicate is a failure result. Otherwise returns the starting result.</summary>
    public static Result Ensure<T>(this Result result, Func<Result<T>> predicate)
    {
        if (result.IsFailed) return result;

        Result<T> predicateResult = predicate();

        if (predicateResult.IsFailed)
            return predicateResult.ToResult();

        return result;
    }
}
#pragma warning restore IDE0046 // Convert to conditional expression
