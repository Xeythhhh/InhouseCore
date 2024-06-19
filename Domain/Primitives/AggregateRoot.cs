namespace Domain.Primitives;

/// <summary>Represents the aggregate root.</summary>
public abstract class AggregateRoot<TEntityId> :
    EntityBase<TEntityId>
    where TEntityId : IEntityId
{
    /// <summary>Initializes a new instance of the <see cref="AggregateRoot"/> class.</summary>
    /// <remarks>Required by EF Core.</remarks>
    protected AggregateRoot() { }

    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>Gets the domain events. This collection is readonly.</summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>Clears all the domain events from the <see cref="AggregateRoot"/>.</summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>Adds the specified <see cref="IDomainEvent"/> to the <see cref="AggregateRoot"/>.</summary>
    /// <param name="domainEvent">The domain event.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
