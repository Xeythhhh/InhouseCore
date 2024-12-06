using SharedKernel.Contracts.v1.Champions.Requests;

using WebApp.Champions.Augments.Abstract;

namespace WebApp.Champions.Augments.Models;

public record AddAugmentModel(long ChampionId) : AugmentModelBase(ChampionId)
{
    public override AddAugmentRequest ToRequest() => new(
        ChampionId,
        AugmentName,
        AugmentTarget,
        AugmentColor,
        AugmentIcon);
}
