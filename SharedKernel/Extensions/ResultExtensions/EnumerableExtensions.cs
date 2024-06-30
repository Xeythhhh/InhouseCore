using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static class EnumerableExtensions
{
    /// <summary>Merge multiple result objects to one result together</summary>
    public static Result Merge(this IEnumerable<Result> results) =>
        Result.MergeInternal(results);

    /// <summary>Merge multiple result objects to one result together</summary>
    public static Result<IEnumerable<TValue>> Merge<TValue>(this IEnumerable<Result<TValue>> results) =>
        Result.MergeInternalWithValue(results);
}