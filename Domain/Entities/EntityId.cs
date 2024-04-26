namespace Domain.Entities;

public abstract record EntityId<TEntityId>(Ulid Value) : IEntityId
{
    public static implicit operator Ulid(EntityId<TEntityId> id) => id.Value;
}
