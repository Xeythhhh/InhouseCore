using Domain.Entities.Users;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Converters.Ids;

public class ApplicationUserIdConverter(ConverterMappingHints? mappingHints = null)
    : IdToStringConverter<ApplicationUserId>(mappingHints);
