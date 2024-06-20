using Domain.Errors;
using Domain.Primitives;

using FluentValidation.Results;

namespace Domain.Champions;

public sealed partial class Champion
{
    private sealed class Errors : DomainErrors
    {
        internal static Error Validation(List<ValidationFailure> errors) =>
            CreateValidationError<Champion>(errors);
    }
}
