using Domain.Champions;
using Domain.Primitives;
using Domain.Primitives.Result;

namespace Infrastructure.Repositories;
public class ChampionRepository(ApplicationDbContext dbContext) :
    IChampionRepository
{
    public Task<Result<Champion>> Add() => throw new NotImplementedException();
    public Task<Result<IEnumerable<Champion>>> GetAll()
    {
        try
        {
            return Task.FromResult(Result.Success(dbContext.Set<Champion>().AsEnumerable()));
        }
        catch (Exception exception)
        {
            return Task.FromResult(
                Result.Failure<IEnumerable<Champion>>(
                    new Error("Database.ChampionRepository.GetAll", exception)));
        }
    }

    public Task<Result<Champion>> GetById() => throw new NotImplementedException();
    public Task<Result<Champion>> GetByName() => throw new NotImplementedException();
    public Task<Result<Champion>> Update() => throw new NotImplementedException();
}
