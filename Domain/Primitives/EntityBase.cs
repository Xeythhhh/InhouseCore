#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the timestamp of when the entity was last updated.</summary>
    public DateTime LastUpdatedAtUtc { get; set; } = DateTime.UtcNow;

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    protected EntityBase() { }

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj is not null &&
        obj.GetType() == GetType() &&
        obj is EntityBase<TEntityId> entity &&
        entity.Id.Value == Id.Value;

    /// <inheritdoc />
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

    /// <inheritdoc />
    public override int GetHashCode() =>
        Id.GetHashCode() * 41;
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
