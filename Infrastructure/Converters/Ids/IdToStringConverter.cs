using Domain.Entities;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Converters.Ids;

public abstract class IdToStringConverter<TId>(ConverterMappingHints? mappingHints = null)
    : ValueConverter<TId, string>(
        id => id.Value.ToString(),
        base32 => (TId)Ulid.Parse(base32),
        DefaultHints.With(mappingHints))
    where TId : EntityId<TId>
{
    static readonly ConverterMappingHints DefaultHints = new(26);
}