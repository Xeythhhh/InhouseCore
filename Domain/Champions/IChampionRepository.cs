using Domain.Abstractions;

using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public interface IChampionRepository :
    IRepository<Champion, Champion.ChampionId>
{
    bool IsNameUnique(Champion champion);
    bool IsNameUnique(string championName);

    public Result Update(Champion champion);

    public Result RemoveChampionRestriction(
        Champion.ChampionId championId,
        ChampionRestriction.RestrictionId restrictionId,
        CancellationToken cancellationToken);
}
