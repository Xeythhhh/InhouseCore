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
            Role = champion.Role.Value,
            Restrictions = champion.Restrictions.ConvertAll(r => r.ToChampionRestrictionDto())
        };

    public static ChampionRestrictionDto ToChampionRestrictionDto(this Champion.Restriction restriction)
        => new()
        {
            RestrictionId = restriction.Id.Value.ToString(),
            Reason = restriction.Reason ?? string.Empty,
            Identifier = restriction.Identifier.Value,
            Color = restriction.ColorHex.Value,
            AbilityName = restriction.AbilityName
        };
}
