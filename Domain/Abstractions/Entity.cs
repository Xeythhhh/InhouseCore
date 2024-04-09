namespace Domain.Abstractions;

/// <summary>
/// Base model for entities.
/// </summary>
public abstract class Entity<TEntityId> : IEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    public TEntityId Id { get; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }

    protected Entity() { }
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

public interface IEntity
{
    IEnumerable<DomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}

public class DomainEvent
{
    //todo
}