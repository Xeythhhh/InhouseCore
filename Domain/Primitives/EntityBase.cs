using System.Diagnostics;

namespace Domain.Primitives;

/// <summary>Base model for entities</summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public abstract class EntityBase<TEntityId> :
    IEntity<TEntityId>,
    IEquatable<EntityBase<TEntityId>>
    where TEntityId :
        IEntityId
{
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

    public override bool Equals(object? obj) =>
        obj is not null &&
        obj.GetType() == GetType() &&
        obj is EntityBase<TEntityId> entity &&
        entity.Id.Value == Id.Value;

    public bool Equals(EntityBase<TEntityId>? other) =>
        other is not null &&
        other.GetType() == GetType() &&
        other.Id.Value == Id.Value;

    public static bool operator ==(EntityBase<TEntityId>? first, EntityBase<TEntityId>? second) =>
        first is not null &&
        second is not null &&
        first.Equals(second);

    public static bool operator !=(EntityBase<TEntityId>? first, EntityBase<TEntityId>? second) =>
        !(first == second);

    public override int GetHashCode() =>
        Id.GetHashCode() * 41;
}

/// <summary>Base type for entity identifiers, providing implicit and explicit conversions to and from long.</summary>
/// <typeparam name="TEntity">The entity type associated with this identifier.</typeparam>
public abstract record EntityId<TEntity>(long Value = default) :
    IEntityId
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
        Type IdType = typeof(TEntity).GetProperty("Id")?.PropertyType
            ?? throw new UnreachableException("All Entities should have an Id");

        return (EntityId<TEntity>)(Activator.CreateInstance(IdType, new object[] { id })
            ?? throw new InvalidCastException($"Can not convert {id} to {IdType.Name}"));
    }
}

/// <summary>Interface representing an entity with a strongly-typed identifier, creation and update timestamps and domain event handling capabilities.</summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public interface IEntity<TEntityId>
    where TEntityId :
        IEntityId
{
    /// <summary>Gets the unique identifier for the entity.</summary>
    public TEntityId Id { get; }
    /// <summary>Gets the timestamp of when the entity was created.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the timestamp of when the entity was last updated.</summary>
    public DateTime LastUpdatedAt { get; set; }
}

/// <summary>Interface for defining a strongly-typed entity identifier.</summary>
public interface IEntityId
{
    /// <summary>Gets the value of the entity identifier.</summary>
    public long Value { get; init; }
}