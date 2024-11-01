using System.Text.RegularExpressions;

using FluentValidation;
using FluentValidation.Results;

using MudBlazor;

using SharedKernel.Extensions.ResultExtensions;

using WebApp.Services;

namespace WebApp.Champions.Augments.Abstract;

public abstract partial record AugmentModelBase
{
    public string ChampionId { get; set; }

    [Label("Name")]
    public string AugmentName { get; set; }

    [Label("Target")]
    public string AugmentTarget { get; set; }

    [Label(name: "Color")]
    public string AugmentColor { get; set; }

    protected AugmentModelBase(
        string championId,
        string augmentName = "",
        string augmentTarget = "",
        string augmentColor = "#FFFFFF")
    {
        ChampionId = championId;
        AugmentName = augmentName;
        AugmentTarget = augmentTarget;
        AugmentColor = augmentColor;
    }

    public abstract object ToRequest();

    public partial class Validator : AbstractValidator<AugmentModelBase>
    {
        private readonly ChampionService _championService;
        private readonly HashSet<string> _validTargets;

        public Validator(ChampionService championService, IConfiguration configuration)
        {
            _championService = championService;
            _validTargets = configuration.GetSection("Domain:AugmentTargets").Get<string[]>()?.ToHashSet()
                ?? new HashSet<string> { "q", "e", "r" };

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(x => x.AugmentName)
                .NotEmpty()
                .Length(1, 100)
                .MustAsync(BeUniqueName)
                .WithMessage("Augment name must be unique.");

            RuleFor(x => x.AugmentTarget)
                .NotEmpty()
                .Length(1, 100)
                .Must(BeValidTarget)
                .WithMessage($"Augment target must be one of the valid options. ({string.Join(", ", _validTargets)})");

            RuleFor(x => x.AugmentColor)
                .NotEmpty()
                .Matches(HexColorWithoutAlphaRegex())
                .WithMessage("Color must be a valid hex code (e.g., #FFFFFF).");
        }

        private async Task<bool> BeUniqueName(AugmentModelBase model, string name, CancellationToken cancellationToken) =>
            (await _championService.GetAugmentNamesAsync(model.ChampionId, cancellationToken)
                .Map(response => response.AugmentNames.Any(augmentName =>
                    augmentName.Equals(name, StringComparison.CurrentCultureIgnoreCase))))
            .Value;

        private bool BeValidTarget(string target) =>
            _validTargets.Any(t =>
                t.Equals(target, StringComparison.CurrentCultureIgnoreCase));

        [GeneratedRegex("^#([0-9A-Fa-f]{6})$")]
        private static partial Regex HexColorWithoutAlphaRegex();

        public Func<AugmentModelBase, string, Task<IEnumerable<string>>> ValidateValue =>
            async (model, propertyName) =>
            {
                ValidationResult result = await ValidateAsync(ValidationContext<AugmentModelBase>.CreateWithOptions(
                    model,
                    x => x.IncludeProperties(propertyName)));

                return result.IsValid
                    ? Array.Empty<string>()
                    : result.Errors.Select(e => e.ErrorMessage);
            };
    }
}
