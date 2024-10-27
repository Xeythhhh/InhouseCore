using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Requests;
using WebApp.Pages.Champions.Dialogs.Restrictions.Abstract;

namespace WebApp.Pages.Champions.Dialogs.Restrictions;

public record AddRestrictionModel(string ChampionId, List<ChampionAugmentDto> Augments)
    : RestrictionModelBase(Augments)
{
    public override AddRestrictionRequest ToRequest() => new(
        ChampionId,
        Augment!.AugmentId,
        Combo?.AugmentId,
        Reason);
}