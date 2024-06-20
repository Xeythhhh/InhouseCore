namespace Domain.Primitives;

/// <summary>Marker Interface for entities</summary>
public interface IEntity;

/// <summary>Interface representing an entity with a strongly-typed identifier, creation and update timestamps and domain event handling capabilities.</summary>
/// <typeparam name="TEntityId">Type of the entity's identifier.</typeparam>
public interface IEntity<TEntityId> :
    IEntity
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
