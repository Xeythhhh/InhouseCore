namespace Domain.Entities;
public abstract record EntityId<TEntityId>(Ulid Value = default) : IEntityId
{
    public static implicit operator Ulid(EntityId<TEntityId> id) => id.Value;
    public static explicit operator EntityId<TEntityId>(Ulid ulid) => (EntityId<TEntityId>)
        (Activator.CreateInstance(typeof(TEntityId), new object[] { ulid })
        ?? throw new InvalidCastException($"Can not convert {ulid} to {nameof(TEntityId)}"));
}
