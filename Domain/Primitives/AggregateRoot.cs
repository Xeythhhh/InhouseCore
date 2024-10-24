namespace Domain.Primitives;

/// <summary>Represents the aggregate root.</summary>
public abstract class AggregateRoot<TEntityId> :
    EntityBase<TEntityId>,
    IAggregateRoot
    where TEntityId : IEntityId
{
    /// <summary>Initializes a new instance of the <see cref="AggregateRoot{TEntity}"/> class.</summary>
    /// <remarks>Required by EF Core.</remarks>
    protected AggregateRoot() { }

    private readonly List<IDomainEvent> _domainEvents = new();

    IReadOnlyCollection<IDomainEvent> IAggregateRoot.GetDomainEvents() => _domainEvents.AsReadOnly();

    void IAggregateRoot.ClearDomainEvents() => _domainEvents.Clear();

    void IAggregateRoot.AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
