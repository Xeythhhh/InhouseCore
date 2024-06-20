using FluentValidation;

namespace Domain.Users;

public sealed partial class ApplicationRole
{
    /// <summary>Validator for <see cref="ApplicationRole"/>.</summary>
    private class Validator : AbstractValidator<ApplicationRole>
    {
        /// <summary>Singleton instance of the validator.</summary>
        public static Validator Instance = new();

        public Validator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
