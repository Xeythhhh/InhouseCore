namespace Domain.Primitives;

/// <summary>Marker Interface for entities</summary>
public interface IEntity
{
    /// <summary>Gets the timestamp of when the entity was created.</summary>
    public DateTime CreatedAtUtc { get; init; }
    /// <summary>Gets or sets the timestamp of when the entity was last updated.</summary>
    public DateTime LastUpdatedAtUtc { get; set; }
}

/// <summary>Interface representing an entity with a strongly-typed identifier</summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public interface IEntity<TEntityId> :
    IEntity
    where TEntityId :
        IEntityId
{
    /// <summary>Gets the unique identifier for the entity.</summary>
    public TEntityId Id { get; }
}
