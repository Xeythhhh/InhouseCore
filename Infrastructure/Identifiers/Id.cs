using Domain;
using Domain.Primitives;

using FluentResults;

using Infrastructure.Exceptions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Identifiers;

/// <summary> Helper class for managing entity identifiers and converters. </summary>
internal static class Id
{
    private static readonly Dictionary<Type, ValueConverter> ValueConverters = new();
    private static int _generatorId;

    /// <summary> Registers an identifier generator with a specified ID. </summary>
    /// <param name="generatorId"> The ID to register with the identifier generator. </param>
    internal static void RegisterGeneratorId(int generatorId) => _generatorId = generatorId;

    /// <summary> Retrieves the registered value converter for a specified type. </summary>
    /// <typeparam name="TId"> The type for which to retrieve the value converter. </typeparam>
    /// <returns> The registered value converter. </returns>
    /// <exception cref="ValueConverterNotRegisteredException"> Thrown when no value converter is registered for the type. </exception>
    internal static ValueConverter GetValueConverter<TId>()
        where TId : IEntityId =>
        ValueConverters.GetValueOrDefault(typeof(TId))
        ?? throw new ValueConverterNotRegisteredException(typeof(TId));

    /// <summary> Retrieves the type of value generator for the specified type and ensures the generator Id has been set </summary>
    /// <typeparam name="TId"> The type for which to retrieve the value generator type. </typeparam>
    /// <returns> The value generator type. </returns>
    internal static Type? GetValueGenerator<TId>()
        where TId : IEntityId
    {
        IdValueGenerator<TId>.SetGeneratorId(_generatorId);
        return typeof(IdValueGenerator<TId>);
    }

    /// <summary> Registers all entity identifier converters from the domain assembly. </summary>
    /// <returns> A result indicating success or failure. </returns>
    internal static Result RegisterConverters()
    {
        try
        {
            IEnumerable<Type> idTypes = DomainAssembly.Reference.GetTypes()
                .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IEntity)))
                .Select(t => t.GetProperty("Id")!.PropertyType);

            foreach (Type? idType in idTypes)
                ValueConverters.TryAdd(idType, CreateConverter(idType));

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(new Error("An Error occurred registering converters.")
                .CausedBy(exception));
        }
    }

    /// <summary> Creates a value converter for the specified type. </summary>
    /// <param name="type"> The type for which to create a value converter. </param>
    /// <returns> The created value converter. </returns>
    private static ValueConverter CreateConverter(Type type) =>
        (ValueConverter)Activator.CreateInstance(
            typeof(IdValueConverter<>).MakeGenericType(type),
            new object?[] { null })!;
}
