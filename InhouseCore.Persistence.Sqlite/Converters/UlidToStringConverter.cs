using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InhouseCore.Persistence.Sqlite.Converters;

/// <summary>
/// <see cref="Ulid"/> to <see cref="string"/> converter for EntityFrameworkCore.
/// </summary>
internal class UlidToStringConverter : ValueConverter<Ulid, string>
{
    private static readonly ConverterMappingHints DefaultHints = new(26);

    /// <summary>
    /// Converts <see cref="Ulid"/> to <see cref="string"/>.
    /// </summary>
    /// <param name="mappingHints">The <see cref="ConverterMappingHints"/>.</param>
    internal UlidToStringConverter(ConverterMappingHints? mappingHints = null)
        : base(
            ulid => ulid.ToString(),
            base32 => base32.Length == 0 ? default : Ulid.Parse(base32),
            DefaultHints.With(mappingHints))
    { }
}
