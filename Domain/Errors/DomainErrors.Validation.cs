using Domain.Primitives;

using FluentValidation.Results;

namespace Domain.Errors;
internal abstract partial class DomainErrors
{
    protected static Error ValidationError = new("Domain.Validation", "Invalid state");

    protected static Error CreateValidationError(IEnumerable<ValidationFailure> errors) =>
        CreateValidationError(errors.Select(e => e.ErrorMessage));

    protected static Error CreateValidationError(IEnumerable<string> errors) => new(
        "Domain.Validation",
        string.Join(", ", errors));

    protected static Error CreateValidationError<T>(IEnumerable<ValidationFailure> errors) =>
        CreateValidationError<T>(errors.Select(e => e.ErrorMessage));

    protected static Error CreateValidationError<T>(IEnumerable<string> errors) => new(
        $"Domain.Validation.{typeof(T).Name}",
        string.Join(", ", errors));
}
