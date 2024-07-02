using Domain.Primitives;

using SharedKernel.Primitives.Result;

namespace Domain.Abstractions;
public interface IRepository;

public interface IRepository<T, TId> :
    IRepository
    where T : IAggregateRoot
    where TId : IEntityId
{
    public Task<Result<List<T>>> GetAll(CancellationToken cancellationToken);

    public Task<Result<List<TOut>>> GetAll<TOut>(Func<T, TOut> converter, CancellationToken cancellationToken);

    public Task<Result<T>> GetById(TId entityId, CancellationToken cancellationToken);

    public Result<List<T>> GetBy(Func<T, bool> predicate);

    public Task<Result<T>> Add(T Aggregate, CancellationToken cancellationToken);

    public Result<T> Delete(T Aggregate);
}
