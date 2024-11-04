using System.Diagnostics;

namespace Domain.Primitives;

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

    public override string ToString() => Value.ToString();
}
