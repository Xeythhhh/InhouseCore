using System.Diagnostics;

namespace Domain.Entities;
public abstract record EntityId<TEntity>(Ulid Value = default) : IEntityId
{
    public static implicit operator Ulid(EntityId<TEntity> id) => id.Value;
    public static explicit operator EntityId<TEntity>(Ulid ulid)
    {
        var IdType = typeof(TEntity).GetProperty("Id")?.PropertyType
            ?? throw new UnreachableException("All Entities should have an Id");

        return (EntityId<TEntity>)(Activator.CreateInstance(IdType, new object[] { ulid })
            ?? throw new InvalidCastException($"Can not convert {ulid} to {IdType.Name}"));
    }
}
