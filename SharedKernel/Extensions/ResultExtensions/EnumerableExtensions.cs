using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;

public static class EnumerableExtensions
{
    /// <summary>Merges multiple <see cref="Result"/> objects into one. Returns a failure if any result is a failure.</summary>
    public static Result Merge(this IEnumerable<Result> results)
        => Result.MergeInternal(results);

    /// <summary> Merges multiple <see cref="Result{T}"/> objects into one containing a collection of values. 
    /// Returns a failure if any result is a failure.</summary>
    /// <typeparam name="TValue">The type of the values contained in the results.</typeparam>
    public static Result<IEnumerable<TValue>> Merge<TValue>(this IEnumerable<Result<TValue>> results)
        => Result.MergeInternalWithValue(results);
}
