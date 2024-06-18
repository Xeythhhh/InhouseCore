using Domain.Entities;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Identifiers;
internal sealed class IdValueConverter<TId, TEntity>(ConverterMappingHints? mappingHints = null)
    : ValueConverter<TId, long>(
        id => id.Value,
        base32 => (TId)base32,
        DefaultHints.With(mappingHints))
    where TId : EntityId<TEntity>
    where TEntity : IEntity<TId>
{
    static readonly ConverterMappingHints DefaultHints = new(26);
}