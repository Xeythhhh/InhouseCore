using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Maps all errors in the result using the specified <paramref name="errorMapper"/>.</summary>
    /// <param name="errorMapper">A function to transform each <see cref="IError"/> in the result.</param>
    /// <returns>A new <see cref="Result"/> with mapped errors.</returns>
    public Result MapErrors(Func<IError, IError> errorMapper) =>
        IsSuccess ? this
                  : new Result()
                        .WithErrors(Errors.Select(errorMapper))
                        .WithSuccesses(Successes);

    /// <summary>Maps all errors in the result using the specified asynchronous <paramref name="errorMapper"/>.</summary>
    public async Task<Result> MapErrorsAsync(Func<IError, Task<IError>> errorMapper)
    {
        if (IsSuccess) return this;

        List<IError> mappedErrors = new();
        foreach (IError error in Errors)
            mappedErrors.Add(await errorMapper(error).ConfigureAwait(false));

        return new Result()
            .WithErrors(mappedErrors)
            .WithSuccesses(Successes);
    }
}
