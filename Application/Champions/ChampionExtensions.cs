using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;

namespace Application.Champions;
public static class ChampionExtensions
{
    public static ChampionDto ToChampionDto(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name.Value,
            Role = champion.Name.Value,
            Restrictions = champion.Restrictions.ConvertAll(r => r.ToChampionRestrictionDto())
        };

    public static ChampionRestrictionDto ToChampionRestrictionDto(this ChampionRestriction restriction) =>
        new()
        {
            Id = restriction.Id,
            DefaultKey = restriction.DefaultKey,
            Name = restriction.Name,
            Reason = restriction.Reason,
        };
}
