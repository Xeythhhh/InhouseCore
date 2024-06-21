namespace Domain.Primitives;

/// <summary>Interface for Aggregate Roots</summary>
public interface IAggregateRoot
{
    /// <summary>Gets the domain events. This collection is readonly.</summary>
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents();

    /// <summary>Clears all the domain events from the <see cref="AggregateRoot"/>.</summary>
    public void ClearDomainEvents();

    /// <summary>Adds the specified <see cref="IDomainEvent"/> to the <see cref="AggregateRoot"/>.</summary>
    /// <param name="domainEvent">The domain event.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent);
}
