using Domain.Primitives;

using Infrastructure.Identifiers;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public abstract class EntityConfiguration<TEntity, TId> :
    IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TId>
    where TId : IEntityId
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasValueGenerator(Id.GetValueGenerator<TId>())
            .HasConversion(Id.GetValueConverter<TId>());
    }
}