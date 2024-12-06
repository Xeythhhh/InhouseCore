using FluentValidation;

using MudBlazor;

using SharedKernel.Extensions.ResultExtensions;

using WebApp.Services;

namespace WebApp.Champions.Augments.Abstract;

/// <summary> Represents the base model for an augment with properties for validation and processing. </summary>
public abstract record AugmentModelBase
{
    /// <summary> Gets or sets the ID of the associated <see cref="Champion"/>. </summary>
    public long ChampionId { get; set; }

    /// <summary> Gets or sets the name of the augment. </summary>
    [Label("Name")]
    public string AugmentName { get; set; }

    /// <summary> Gets or sets the target for the augment (e.g., abilities like Q, W, etc.). </summary>
    [Label("Target")]
    public string AugmentTarget { get; set; }

    /// <summary> Gets or sets the hex color representing the augment. </summary>
    [Label(name: "Color")]
    public string AugmentColor { get; set; }

    /// <summary> Gets or sets the icon identifier for the augment. </summary>
    [Label(name: "Icon")]
    public string AugmentIcon { get; set; }

    /// <summary> Gets or sets the human-readable version of the augment's color. </summary>
    [Label(name: "HumanizedColor")]
    public string? AugmentColorHumanized { get; set; }

    /// <summary> Initializes a new instance of <see cref="AugmentModelBase"/> with default values. </summary>
    protected AugmentModelBase(
        long championId,
        string augmentName = "",
        string augmentTarget = "",
        string augmentColor = "#FFFFFF",
        string augmentIcon = "")
    {
        ChampionId = championId;
        AugmentName = augmentName;
        AugmentTarget = augmentTarget;
        AugmentColor = augmentColor;
        AugmentIcon = augmentIcon;
    }

    /// <summary> Converts the model to a request object for further processing. </summary>
    public abstract object ToRequest();

    /// <summary> Validator for <see cref="AugmentModelBase"/> ensuring data integrity and business rules. </summary>
    public class Validator : AbstractValidator<AugmentModelBase>
    {
        private readonly ChampionService _championService;
        private IReadOnlyCollection<string> _namesInUse;
        private IReadOnlyCollection<string> _validTargets;
        private IReadOnlyDictionary<string, string> _validColors;

        private string ValidTargetsString => string.Join(", ", _validTargets);
        private string ValidColorsString => string.Join(", ", _validColors);

        /// <summary> Initializes the validator with rules for <see cref="AugmentModelBase"/>. </summary>
        /// <param name="championService">The <see cref="ChampionService"/> for fetching champion-related data.</param>
        public Validator(ChampionService championService)
        {
            _championService = championService;

            RuleFor(x => x.ChampionId)
                .GreaterThan(0).WithMessage("ChampionId must be a valid, non-zero identifier.")
                .CustomAsync(async (championId, _, cancellationToken) =>
                    await InitializeReferenceData(championId, cancellationToken));

            RuleFor(x => x.AugmentName)
                .NotEmpty().WithMessage("Augment name is required.")
                .Length(1, 100).WithMessage("Augment name must be between 1 and 100 characters.")
                .Must(BeUniqueName).WithMessage("Augment name must be unique.");

            RuleFor(x => x.AugmentTarget)
                .NotEmpty().WithMessage("Augment target is required.")
                .Length(1, 100).WithMessage("Augment target must be between 1 and 100 characters.")
                .Must(BeValidTarget).WithMessage($"Augment target must be one of the following: {ValidTargetsString}.");

            RuleFor(x => x.AugmentColor)
                .NotEmpty().WithMessage("Augment color is required.")
                .Length(7).WithMessage("Augment color must be a valid hex code (e.g., #FFFFFF).")
                .Must(BeValidColor).WithMessage($"Augment color must be one of the following: {ValidColorsString}.");

            RuleFor(x => x.AugmentIcon)
                .NotEmpty().WithMessage("Augment icon is required.")
                .Must(BeValidUrl).WithMessage("Augment icon must be a valid URL.");
        }

        /// <summary> Initializes valid targets and colors for the champion. </summary>
        private async Task InitializeReferenceData(long championId, CancellationToken cancellationToken) =>
            await _championService.GetAvailableAugmentTargetsAndColorsAsync(championId, cancellationToken)
                .Tap(targetsAndColors =>
                {
                    _validTargets = targetsAndColors.AugmentTargets;
                    _validColors = targetsAndColors.AugmentColors;
                })
                .Bind(_ => _championService.GetAugmentNamesAsync(championId, cancellationToken))
                .Tap(response => _namesInUse = response.AugmentNames)
                .TapError(_ => throw new InvalidOperationException("Failed to initialize reference data."));

        /// <summary> Checks if the given name is unique among the champion's augments. </summary>
        private bool BeUniqueName(string name) =>
            _namesInUse.All(augmentName =>
                    !augmentName.Equals(name, StringComparison.CurrentCultureIgnoreCase));

        /// <summary> Checks if the target is valid based on the predefined set of valid targets. </summary>
        private bool BeValidTarget(string target) =>
            _validTargets.Contains(target, StringComparer.OrdinalIgnoreCase);

        /// <summary> Checks if the color is valid based on the predefined set of valid colors. </summary>
        private bool BeValidColor(string color) =>
            _validColors.Values.Contains(color, StringComparer.OrdinalIgnoreCase);

        /// <summary> Checks if the URL is valid. </summary>
        private bool BeValidUrl(string url)
        {
            url = url.Trim();
            return Uri.TryCreate(url, UriKind.Absolute, out _) &&
                   url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
                   url.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

    }
}
