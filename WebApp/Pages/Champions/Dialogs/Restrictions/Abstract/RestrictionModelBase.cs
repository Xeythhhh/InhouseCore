using FluentValidation;
using FluentValidation.Results;

using MudBlazor;
using SharedKernel.Contracts.v1.Champions.Dtos;

namespace WebApp.Pages.Champions.Dialogs.Restrictions.Abstract;

public abstract record RestrictionModelBase
{
    [Label("Reason")]
    public string Reason { get; set; }
    public ChampionAugmentDto? Augment { get; set; }
    public ChampionAugmentDto? Combo { get; set; }

    public List<ChampionAugmentDto> Augments { get; init; }

    public RestrictionModelBase(
        List<ChampionAugmentDto> augments,
        ChampionAugmentDto? augment = null,
        ChampionAugmentDto? combo = null,
        string reason = "")
    {
        Augments = augments;
        Reason = reason;
        Augment = augment;
        Combo = combo;
    }

    public abstract object ToRequest();

    public class Validator : AbstractValidator<RestrictionModelBase>
    {
        public Validator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty()
                .Length(1, 100);
        }

        public Func<RestrictionModelBase, string, Task<IEnumerable<string>>> ValidateValue =>
            async (model, propertyName) =>
            {
                ValidationResult result = await ValidateAsync(ValidationContext<RestrictionModelBase>.CreateWithOptions(
                    model,
                    x => x.IncludeProperties(propertyName)));

                return result.IsValid
                    ? (IEnumerable<string>)Array.Empty<string>()
                    : result.Errors.Select(e => e.ErrorMessage);
            };
    }
}
