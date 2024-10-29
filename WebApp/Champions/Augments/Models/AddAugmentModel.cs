using SharedKernel.Contracts.v1.Champions.Requests;

using WebApp.Champions.Augments.Abstract;

namespace WebApp.Champions.Augments.Models;

public record AddAugmentModel(string ChampionId) : AugmentModelBase(ChampionId)
{
    public override AddAugmentRequest ToRequest() => new(
        ChampionId,
        AugmentName,
        AugmentTarget,
        AugmentColor);
}
