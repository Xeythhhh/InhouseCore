using Domain;
using Domain.Primitives;
using Domain.Primitives.Result;
using Domain.Users;
using Infrastructure.Exceptions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Identifiers;

/// <summary> Helper class for managing entity identifiers and converters. </summary>
internal static class Id
{
    private static readonly Dictionary<Type, ValueConverter> ValueConverters = new();

    /// <summary> Retrieves the registered value converter for a specified type. </summary>
    /// <typeparam name="T"> The type for which to retrieve the value converter. </typeparam>
    /// <returns> The registered value converter. </returns>
    /// <exception cref="ValueConverterNotRegisteredException"> Thrown when no value converter is registered for the type. </exception>
    internal static ValueConverter GetValueConverter<T>() =>
        ValueConverters.GetValueOrDefault(typeof(T))
        ?? throw new ValueConverterNotRegisteredException(typeof(T));

    /// <summary> Registers all entity identifier converters from the domain assembly. </summary>
    internal static void RegisterConverters()
    {
        IEnumerable<Type> entityTypes = DomainAssembly.Reference.GetTypes()
            .Where(t => !t.IsAbstract && IsAssignableToGenericType(t, typeof(IEntity<>)));

        // Exclude ApplicationRole due to invariance in Identity implementation
        entityTypes = entityTypes.Where(t => t != typeof(ApplicationRole));
        ValueConverters.Add(typeof(ApplicationRole), new IdValueConverter<AspNetIdentityId, ApplicationUser>());

        foreach (Type? type in entityTypes)
            ValueConverters.Add(type, CreateConverter(type));
    }

    /// <summary> Registers an identifier generator with a specified ID. </summary>
    /// <param name="generatorId"> The ID to register with the identifier generator. </param>
    internal static void RegisterGenerator(int generatorId)
    {
        Result result = IdValueGenerator.Register(generatorId);
        if (result.IsFailure) throw new InvalidOperationException(result.Error); //TODO
    }

    private static ValueConverter CreateConverter(Type type)
    {
        Type idType = type.GetProperty("Id")!.PropertyType;
        return (ValueConverter)Activator.CreateInstance(
            typeof(IdValueConverter<,>).MakeGenericType(idType, type),
            new object?[] { null })!;
    }

    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        foreach (Type it in givenType.GetInterfaces())
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
