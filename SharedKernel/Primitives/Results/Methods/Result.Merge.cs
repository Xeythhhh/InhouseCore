using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Merge multiple result objects to one result object together</summary>
    public static Result Merge(params ResultBase[] results) =>
        MergeInternal(results);

    /// <summary>Merge multiple result objects to one result object together. Return one result with a list of merged values.</summary>
    public static Result<IEnumerable<TValue>> Merge<TValue>(params Result<TValue>[] results) =>
        MergeInternalWithValue(results);

    internal static Result MergeInternal(IEnumerable<ResultBase> results) =>
        Ok().WithReasons(results.SelectMany(result => result.Reasons));

    internal static Result<IEnumerable<TValue>> MergeInternalWithValue<TValue>(
        IEnumerable<Result<TValue>> results)
    {
        List<Result<TValue>> resultList = results.ToList();

        Result<IEnumerable<TValue>> finalResult =
            Ok<IEnumerable<TValue>>(new List<TValue>())
            .WithReasons(resultList.SelectMany(result => result.Reasons));

        if (finalResult.IsSuccess)
            finalResult.WithValue(resultList.ConvertAll(r => r.Value));

        return finalResult;
    }
}
