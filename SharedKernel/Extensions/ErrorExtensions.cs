using FluentResults;

using FluentValidation.Results;

namespace SharedKernel.Extensions;

/// <summary>Contains extension methods for the Error class</summary>
public static class ErrorExtensions
{
    /// <summary>Adds validation result details to the error and returns a new error caused by the validation errors</summary>
    /// <param name="error">The original error</param>
    /// <param name="validationResult">The validation result</param>
    /// <returns>A new error caused by the validation errors</returns>
    public static Error CausedBy(this Error error, ValidationResult validationResult)
    {
        error.WithMetadata(typeof(ValidationResult).Name, validationResult);

        List<Error> errors = validationResult.Errors
            .ConvertAll(e => new Error(
                $"{e.ErrorCode}: {e.ErrorMessage}"));

        return error.CausedBy(errors);
    }
}
