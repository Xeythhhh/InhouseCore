using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;

namespace Application.Champions;
public static class ChampionExtensions
{
    public static ChampionDto ToChampionDto(this Champion champion)
        => new()
        {
            Id = champion.Id.Value.ToString(),
            Name = champion.Name.Value,
            Role = champion.Name.Value,
            Restrictions = champion.Restrictions.ConvertAll(r => r.ToChampionRestrictionDto())
        };

    public static ChampionRestrictionDto ToChampionRestrictionDto(this ChampionRestriction restriction)
        => new()
        {
            RestrictionId = restriction.Id.Value.ToString(),
            Target = restriction.Target.Value,
            Reason = restriction.Reason.Value,
            Name = restriction.Target.Name.Value,
            Identifier = restriction.Target.Name.Value
        };
}
