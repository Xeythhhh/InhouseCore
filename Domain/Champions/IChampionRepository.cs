using Domain.Abstractions;

using FluentResults;

namespace Domain.Champions;

public interface IChampionRepository :
    IRepository<Champion, ChampionId>
{
    public Result<Champion> Update(Champion champion);
}
