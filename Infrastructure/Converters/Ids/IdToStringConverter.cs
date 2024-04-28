using Domain.Entities;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Converters.Ids;

public class IdToStringConverter<TId, TEntity>(ConverterMappingHints? mappingHints = null)
    : ValueConverter<TId, string>(
        id => id.Value.ToString(),
        base32 => (TId)Ulid.Parse(base32),
        DefaultHints.With(mappingHints))
    where TId : EntityId<TId>
    where TEntity : IEntity<TId>
{
    static readonly ConverterMappingHints DefaultHints = new(26);
}