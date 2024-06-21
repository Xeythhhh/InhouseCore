using Domain.Primitives;

using FluentResults;

namespace Domain.Abstractions;
public interface IRepository;

public interface IRepository<T, TId> :
    IRepository
    where T : IAggregateRoot
    where TId : IEntityId
{
    public Task<Result<List<T>>> GetAll();
    public Task<Result<T>> GetById(TId entityId);
    public Result<List<T>> GetBy(Func<T, bool> predicate);
    public Task<Result<T>> Add(T Aggregate);
    public Result<T> Delete(T Aggregate);
}
