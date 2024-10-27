using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Contracts.v1.Champions.Dtos;
using WebApp.Pages.Champions.Dialogs.Restrictions.Abstract;

namespace WebApp.Pages.Champions.Dialogs.Restrictions;

public record UpdateRestrictionModel(
    string RestrictionId,
    List<ChampionAugmentDto> Augments,
    ChampionAugmentDto? Augment,
    ChampionAugmentDto? Combo,
    string Reason)
    : RestrictionModelBase(Augments, Augment, Combo, Reason)
{
    public override UpdateRestrictionRequest ToRequest() => new(
        RestrictionId,
        Augment!.AugmentId,
        Combo?.AugmentId,
        Reason);
}