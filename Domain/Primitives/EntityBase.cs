using System.Diagnostics;

namespace Domain.Entities;

/// <summary>Base model for entities, providing core functionality such as domain event handling and timestamp management.</summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public abstract class EntityBase<TEntityId>
    : IEntity<TEntityId>
    where TEntityId : IEntityId
{
    /// <summary>A list of domain events associated with the entity./// </summary>
    protected readonly List<DomainEvent> _domainEvents = new();

    /// <summary>Gets the unique identifier for the entity.</summary>
    public TEntityId Id { get; protected set; }
    /// <summary>Gets the timestamp of when the entity was created.</summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the timestamp of when the entity was last updated.</summary>
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    protected EntityBase() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Gets the domain events associated with the entity.</summary>
    /// <returns>A collection of domain events.</returns>
    public virtual IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    /// <summary>Clears the domain events associated with the entity.</summary>
    public virtual void ClearDomainEvents() => _domainEvents.Clear();
    /// <summary>Raises a new domain event.</summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    public virtual void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

/// <summary>Base type for entity identifiers, providing implicit and explicit conversions to and from long.</summary>
/// <typeparam name="TEntity">The entity type associated with this identifier.</typeparam>
public abstract record EntityId<TEntity>(long Value = default)
    : IEntityId
{
    /// <summary>Implicitly converts an <see cref="EntityId{TEntity}"/> to a long.</summary>
    /// <param name="id">The entity identifier to convert.</param>
    public static implicit operator long(EntityId<TEntity> id) => id.Value;

    /// <summary>Explicitly converts a long to an <see cref="EntityId{TEntity}"/>.</summary>
    /// <param name="id">The long value to convert.</param>
    /// <returns>The converted entity identifier.</returns>
    /// <exception cref="InvalidCastException">Thrown when the conversion fails.</exception>
    public static explicit operator EntityId<TEntity>(long id)
    {
        var IdType = typeof(TEntity).GetProperty("Id")?.PropertyType
            ?? throw new UnreachableException("All Entities should have an Id");

        return (EntityId<TEntity>)(Activator.CreateInstance(IdType, new object[] { id })
            ?? throw new InvalidCastException($"Can not convert {id} to {IdType.Name}"));
    }
}

/// <summary>Interface representing an entity with a strongly-typed identifier, creation and update timestamps and domain event handling capabilities.</summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public interface IEntity<TEntityId>
    where TEntityId : IEntityId
{
    /// <summary>Gets the unique identifier for the entity.</summary>
    public TEntityId Id { get; }
    /// <summary>Gets the timestamp of when the entity was created.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the timestamp of when the entity was last updated.</summary>
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>Gets the domain events associated with the entity.</summary>
    /// <returns>A collection of domain events.</returns>
    public IEnumerable<DomainEvent> GetDomainEvents();
    /// <summary>Clears the domain events associated with the entity.</summary>
    public void ClearDomainEvents();
    /// <summary>Raises a new domain event.</summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    public void RaiseEvent(DomainEvent domainEvent);
}

/// <summary>Interface for defining a strongly-typed entity identifier.</summary>
public interface IEntityId
{
    /// <summary>Gets the value of the entity identifier.</summary>
    public long Value { get; init; }
}

public class DomainEvent
{
    //todo
}