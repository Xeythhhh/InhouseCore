using System.Diagnostics;

namespace Domain.Entities;

/// <summary>
/// Base model for entities, providing core functionality such as domain event handling and timestamp management.
/// </summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public abstract class EntityBase<TEntityId>
    : IEntity<TEntityId>
    where TEntityId : IEntityId
{
    protected readonly List<DomainEvent> _domainEvents = new();

    public TEntityId Id { get; protected set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected EntityBase()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // Private constructor required by EF Core and auto-mappings
    }

    public virtual IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public virtual void ClearDomainEvents() => _domainEvents.Clear();
    public virtual void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

/// <summary>
/// Base type for entity identifiers, providing implicit and explicit conversions to and from long.
/// </summary>
/// <typeparam name="TEntity">The entity type associated with this identifier.</typeparam>
public abstract record EntityId<TEntity>(long Value = default)
    : IEntityId
{
    public static implicit operator long(EntityId<TEntity> id) => id.Value;
    public static explicit operator EntityId<TEntity>(long id)
    {
        var IdType = typeof(TEntity).GetProperty("Id")?.PropertyType
            ?? throw new UnreachableException("All Entities should have an Id");

        return (EntityId<TEntity>)(Activator.CreateInstance(IdType, new object[] { id })
            ?? throw new InvalidCastException($"Can not convert {id} to {IdType.Name}"));
    }
}

/// <summary>
/// Interface representing an entity with a strongly-typed identifier, creation and update timestamps and domain event handling capabilities.
/// </summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public interface IEntity<TEntityId>
    where TEntityId : IEntityId
{
    public TEntityId Id { get; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; set; }

    public IEnumerable<DomainEvent> GetDomainEvents();
    public void ClearDomainEvents();
    public void RaiseEvent(DomainEvent domainEvent);
}

/// <summary>Interface for defining a strongly-typed entity identifier.</summary>
public interface IEntityId
{
    public long Value { get; init; }
}

public class DomainEvent
{
    //todo
}