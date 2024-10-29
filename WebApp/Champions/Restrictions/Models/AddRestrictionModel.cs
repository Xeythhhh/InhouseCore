using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Requests;

using WebApp.Champions.Restrictions.Abstract;

namespace WebApp.Champions.Restrictions.Models;

public record AddRestrictionModel(string ChampionId, List<ChampionAugmentDto> Augments)
    : RestrictionModelBase(Augments)
{
    public override AddRestrictionRequest ToRequest() => new(
        ChampionId,
        Augment!.AugmentId,
        Combo?.AugmentId,
        Reason);
}