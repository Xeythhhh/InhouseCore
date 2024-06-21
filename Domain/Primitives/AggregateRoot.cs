namespace Domain.Primitives;

/// <summary>Represents the aggregate root.</summary>
public abstract class AggregateRoot<TEntityId> :
    EntityBase<TEntityId>,
    IAggregateRoot
    where TEntityId :
        IEntityId
{
    /// <summary>Initializes a new instance of the <see cref="AggregateRoot"/> class.</summary>
    /// <remarks>Required by EF Core.</remarks>
    protected AggregateRoot() { }

    private readonly List<IDomainEvent> _domainEvents = new();

    /// <inheritdoc/>
    IReadOnlyCollection<IDomainEvent> IAggregateRoot.GetDomainEvents() => _domainEvents.AsReadOnly();

    /// <inheritdoc/>
    void IAggregateRoot.ClearDomainEvents() => _domainEvents.Clear();

    /// <inheritdoc/>
    void IAggregateRoot.AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
