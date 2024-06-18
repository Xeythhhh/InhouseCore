using Domain;
using Domain.Entities;
using Domain.Entities.Users;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Identifiers;
/// <summary>Helper class for Identifiers</summary>
internal static class Id
{
    internal static readonly Dictionary<Type, ValueConverter> ValueConverters = new();

    internal static void RegisterConverters()
    {
        var entityTypes = DomainAssembly.Reference.GetTypes()
            .Where(t => !t.IsAbstract && IsAssignableToGenericType(t, typeof(IEntity<>)));

        // ApplicationRole invariance due to Identity Implementation :/
        entityTypes = entityTypes.Where(t => t != typeof(ApplicationRole));
        ValueConverters.Add(typeof(ApplicationRole),
            new IdValueConverter<AspNetIdentityId, ApplicationUser>());

        foreach (var type in entityTypes)
            ValueConverters.Add(type, CreateConverter(type));
    }

    internal static void RegisterGenerator(int generatorId)
    {
        var result = IdValueGenerator.Create(generatorId);
        if (result.IsFailure) throw new InvalidOperationException(result.Error); //TODO
    }

    private static ValueConverter CreateConverter(Type type)
    {
        var idType = type.GetProperty("Id")!.PropertyType;
        return (ValueConverter)Activator.CreateInstance(
            typeof(IdValueConverter<,>).MakeGenericType(idType, type),
            new object?[] { null })!;
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