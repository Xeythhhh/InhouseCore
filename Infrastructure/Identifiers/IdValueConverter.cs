using Domain.Entities;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Identifiers;

/// <summary> <see cref="ValueConverter{TModel, TProvider}"/> for converting between <see cref="EntityId{TEntity}"/> and their underlying <see cref="long"/> values. </summary>
/// <remarks> Initializes a new instance of the <see cref="IdValueConverter{TId, TEntity}"/> class. </remarks>
/// <param name="mappingHints"> Optional <see cref="ConverterMappingHints"/> to configure the converter. </param>
internal sealed class IdValueConverter<TId, TEntity>(ConverterMappingHints? mappingHints = null) :
    ValueConverter<TId, long>(
    id => id.Value,
    base32 => (TId)base32,
    DefaultHints.With(mappingHints))

    where TId :
        EntityId<TEntity>

    where TEntity :
        IEntity<TId>
{
    private static readonly ConverterMappingHints DefaultHints = new(26);
}
