using Domain.Abstractions;
using Domain.Primitives.Result;

namespace Domain.Champions;

public interface IChampionRepository : IRepository<Champion>
{
    public Task<Result<IEnumerable<Champion>>> GetAll();
    public Task<Result<Champion>> GetById();
    public Task<Result<Champion>> GetByName();
    public Task<Result<Champion>> Add();
    public Task<Result<Champion>> Update();
}
