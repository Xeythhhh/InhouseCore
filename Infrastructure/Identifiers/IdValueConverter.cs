using Domain.Primitives;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Identifiers;

/// <summary> <see cref="ValueConverter{TModel, TProvider}"/> for converting between <see cref="EntityId{IEntity}"/> and their underlying <see cref="long"/> values. </summary>
/// <remarks> Initializes a new instance of the <see cref="IdValueConverter{TId}"/> class. </remarks>
/// <param name="mappingHints"> Optional <see cref="ConverterMappingHints"/> to configure the converter. </param>
internal sealed class IdValueConverter<TId>(ConverterMappingHints? mappingHints = null) :
    ValueConverter<TId, long>(
    id => id.Value,
    value => (TId)Activator.CreateInstance(typeof(TId), value)!,
    DefaultHints.With(mappingHints))
    where TId : IEntityId
{
    private static readonly ConverterMappingHints DefaultHints = new(26);
}
