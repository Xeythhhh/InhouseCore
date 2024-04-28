using Domain;
using Domain.Entities;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Converters.Ids;

public class IdConverters : Dictionary<Type, ValueConverter>
{
    public IdConverters()
    {
        var entityTypes = DomainAssembly.Reference.GetTypes()
            .Where(t => !t.IsAbstract && IsAssignableToGenericType(t, typeof(IEntity<>)));

        foreach (var type in entityTypes)
        {
            var idType = type.GetProperty("Id")!.PropertyType;
            ValueConverter converter = (ValueConverter)Activator.CreateInstance(
                typeof(IdToStringConverter<>).MakeGenericType(idType),
                new object?[] { null })!;

            Add(type, converter);
        }
    }

    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        foreach (var it in givenType.GetInterfaces())
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;

        Type? baseType = givenType.BaseType;
        return baseType != null
            && IsAssignableToGenericType(baseType, genericType);
    }
}
