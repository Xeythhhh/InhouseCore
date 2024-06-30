using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    /// <summary>Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic</summary>
    public static async Task<Result<TNew>> Map<TOld, TNew>(this Task<Result<TOld>> resultTask, Func<TOld, TNew> valueConverter) =>
        (await resultTask).Map(valueConverter);

    /// <summary>Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic</summary>
    public static async Task<Result<TNew>> Map<TOld, TNew>(this Result<Task<TOld>> resultTask, Func<TOld, TNew> valueConverter) =>
            await Result.Try(async () => await resultTask.Value)
                .Map(valueConverter);
}
