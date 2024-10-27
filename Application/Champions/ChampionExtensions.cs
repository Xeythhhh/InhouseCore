using Domain.Champions;

using SharedKernel.Contracts.v1.Champions.Dtos;

namespace Application.Champions;
public static class ChampionExtensions
{
    public static ChampionDto ToChampionDto(this Champion champion) => new(
        champion.Id.Value.ToString(),
        champion.Name.Value,
        champion.Role.Value,
        champion.HasRestrictions,
        champion.Augments.ConvertAll(r => r.ToChampionAugmentDto()),
        champion.Restrictions.ConvertAll(r => r.ToChampionRestrictionDto()));

    public static ChampionRestrictionDto ToChampionRestrictionDto(this Champion.Restriction restriction) => new(
        restriction.Id.Value.ToString(),
        restriction.Augment.Id.Value.ToString(),
        restriction.Augment2?.Id.Value.ToString(),
        restriction.Reason ?? string.Empty);

    public static ChampionAugmentDto ToChampionAugmentDto(this Champion.Augment augment) => new(
        augment.Id.Value.ToString(),
        augment.Name,
        augment.Target.Value,
        augment.ColorHex.Value);
}
