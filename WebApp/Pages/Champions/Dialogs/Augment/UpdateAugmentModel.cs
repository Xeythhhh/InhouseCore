using SharedKernel.Contracts.v1.Champions;

using WebApp.Pages.Champions.Dialogs.Augment.Abstract;

namespace WebApp.Pages.Champions.Dialogs;

public record UpdateAugmentModel(
    string ChampionId,
    string AugmentId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor)
    : AugmentModelBase(
        ChampionId,
        AugmentName,
        AugmentTarget,
        AugmentColor)
{
    public override UpdateAugmentRequest ToRequest() => new(
        AugmentId,
        AugmentName,
        AugmentTarget,
        AugmentColor);
}