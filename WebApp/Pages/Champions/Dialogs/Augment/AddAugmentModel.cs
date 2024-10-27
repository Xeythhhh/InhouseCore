using SharedKernel.Contracts.v1.Champions.Requests;

using WebApp.Pages.Champions.Dialogs.Augment.Abstract;

namespace WebApp.Pages.Champions.Dialogs;

public record AddAugmentModel(string ChampionId) : AugmentModelBase(ChampionId)
{
    public override AddAugmentRequest ToRequest() => new(
        ChampionId,
        AugmentName,
        AugmentTarget,
        AugmentColor);
}
