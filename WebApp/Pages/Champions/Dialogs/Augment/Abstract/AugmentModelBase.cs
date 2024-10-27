using System.Net.Http.Json;

using FluentValidation;
using FluentValidation.Results;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Responses;

namespace WebApp.Pages.Champions.Dialogs.Augment.Abstract;

public abstract record AugmentModelBase
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

    public class Validator : AbstractValidator<AugmentModelBase>
    {
        private const string IsHexColorWithoutAlphaChannelRegex = "^#([0-9A-Fa-f]{6})$";
        private readonly HttpClient _httpClient;
        private readonly HashSet<string> _validTargets;

        public Validator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _validTargets = configuration.GetSection("Domain:AugmentTargets").Get<string[]>()?.ToHashSet()
                ?? new HashSet<string> { "q", "e", "r" };

            RuleFor(x => x.AugmentName)
                .NotEmpty()
                .Length(1, 100)
                .MustAsync((model, name, cancellationToken) =>
                    BeUniqueName(model.ChampionId, name, cancellationToken))
                .WithMessage("Augment name must be unique.");

            RuleFor(x => x.AugmentTarget)
                .NotEmpty()
                .Length(1, 100)
                .Must(target => _validTargets.Contains(target))
                .WithMessage($"Augment target must be one of the valid options. ({string.Join(", ", _validTargets)})");

            RuleFor(x => x.AugmentColor)
                .NotEmpty()
                .Matches(IsHexColorWithoutAlphaChannelRegex)
                .WithMessage("Color must be a valid hex code (e.g., #FFFFFF).");
        }

        private async Task<bool> BeUniqueName(string championId, string name, CancellationToken cancellationToken)
        {
            GetChampionAugmentNamesResponse result = await _httpClient.GetFromJsonAsync<GetChampionAugmentNamesResponse>(
                $"champions/augment-names/{championId}", cancellationToken: cancellationToken)
                ?? throw new HttpRequestException($"Failed to retrieve augment names for ChampionId: {championId}");

            return !result.AugmentNames.Any(augmentName =>
                augmentName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

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