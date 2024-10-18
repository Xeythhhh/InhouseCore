using Domain.Abstractions;

using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public interface IChampionRepository :
    IRepository<Champion, Champion.ChampionId>
{
    bool CheckIsNameUnique(Champion champion);
    bool CheckIsNameUnique(string championName);
    public Result<Champion> Update(Champion champion);
}
