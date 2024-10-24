using Domain.Primitives;

using SharedKernel.Primitives.Result;

namespace Domain.Abstractions;

/// <summary>Marker interface for repository pattern implementations.</summary>
public interface IRepository;

/// <summary>Base interface for repository pattern implementations.</summary>
/// <typeparam name="T">The type of the aggregate root entity.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IRepository<T, TId> : IRepository
    where T : IAggregateRoot
    where TId : IEntityId
{
    /// <summary>Retrieves all entities of type <typeparamref name="T"/>.</summary>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>A <see cref="Result"/> containing a list of all entities of type <typeparamref name="T"/>.</returns>
    Task<Result<List<T>>> GetAll(CancellationToken cancellationToken);

    /// <summary>Retrieves all entities of type <typeparamref name="T"/> and converts them to <typeparamref name="TOut"/>.</summary>
    /// <param name="converter">A function to convert <typeparamref name="T"/> to <typeparamref name="TOut"/>.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>A <see cref="Result"/> containing a list of converted entities of type <typeparamref name="TOut"/>.</returns>
    Task<Result<List<TOut>>> GetAll<TOut>(Func<T, TOut> converter, CancellationToken cancellationToken);

    /// <summary>Retrieves an entity of type <typeparamref name="T"/> by its identifier.</summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>A <see cref="Result"/> containing the entity of type <typeparamref name="T"/> if found; otherwise, an error.</returns>
    Task<Result<T>> GetById(TId entityId, CancellationToken cancellationToken);

    /// <summary>Retrieves entities of type <typeparamref name="T"/> that match a specified predicate.</summary>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <returns>A <see cref="Result"/> containing a list of entities that match the specified predicate.</returns>
    Result<List<T>> GetBy(Func<T, bool> predicate);

    /// <summary>Adds a new entity of type <typeparamref name="T"/> to the repository.</summary>
    /// <param name="Aggregate">The aggregate to add.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>A <see cref="Result"/> containing the added entity.</returns>
    Task<Result<T>> Add(T Aggregate, CancellationToken cancellationToken);

    /// <summary>Deletes the specified entity of type <typeparamref name="T"/> from the repository.</summary>
    /// <param name="Aggregate">The aggregate to delete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the deletion operation.</returns>
    Result Delete(T Aggregate);
}
