namespace Domain.Entities;

/// <summary>Base model for entities.</summary>
public abstract class EntityBase<TEntityId> : IEntity<TEntityId>
    where TEntityId : IEntityId
{
    protected readonly List<DomainEvent> _domainEvents = new();

    public TEntityId Id { get; protected set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected EntityBase() { }         // Used by EFCore 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public virtual IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public virtual void ClearDomainEvents() => _domainEvents.Clear();
    public virtual void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

public class DomainEvent
{
    //todo
}