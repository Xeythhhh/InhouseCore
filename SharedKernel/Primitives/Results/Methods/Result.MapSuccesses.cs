using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Maps all successes in the result using the specified <paramref name="successMapper"/>.</summary>
    /// <param name="successMapper">A function to transform each <see cref="ISuccess"/> in the result.</param>
    /// <returns>A new <see cref="Result"/> with mapped successes.</returns>
    public Result MapSuccesses(Func<ISuccess, ISuccess> successMapper) =>
        new Result()
            .WithErrors(Errors)
            .WithSuccesses(Successes.Select(successMapper));

    /// <summary>Maps all successes in the result using the specified asynchronous <paramref name="successMapper"/>.</summary>
    public async Task<Result> MapSuccessesAsync(Func<ISuccess, Task<ISuccess>> successMapper)
    {
        List<ISuccess> mappedSuccesses = new();
        foreach (ISuccess success in Successes)
            mappedSuccesses.Add(await successMapper(success).ConfigureAwait(false));

        return new Result()
            .WithErrors(Errors)
            .WithSuccesses(mappedSuccesses);
    }
}
