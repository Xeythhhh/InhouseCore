using Domain.Abstractions;

using FluentResults;

namespace Domain.Champions;

public interface IChampionRepository :
    IRepository<Champion, ChampionId>
{
    bool CheckIsNameUnique(Champion champion);
    bool CheckIsNameUnique(string championName);
    public Result<Champion> Update(Champion champion);
}
