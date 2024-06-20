using FluentValidation;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Validator for <see cref="Champion"/> instances.</summary>
    private sealed class Validator
        : AbstractValidator<Champion>
    {
        /// <summary>Gets a singleton instance of the <see cref="Validator"/> class.</summary>
        public static Validator Instance { get; } = new();

        /// <summary>Initializes a new instance of the <see cref="Validator"/> class.</summary>
        private Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("'Name' must not be empty.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            RuleFor(x => x.Class)
                .IsInEnum().WithMessage("Class must be a valid enum value.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Role must be a valid enum value.");
        }
    }
}
