using System.Diagnostics;

namespace Domain.Entities;
/// <summary>Base type for entityIds.</summary>
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
