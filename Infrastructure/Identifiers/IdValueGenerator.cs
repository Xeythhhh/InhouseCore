using Domain.Primitives;

using IdGen;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Identifiers;

/// <summary> Value generator for generating unique identifier values using IdGen. </summary>
internal sealed class IdValueGenerator<TId>() :
    ValueGenerator<TId>
    where TId : IEntityId
{
    private static IdGenerator? _generator;

    /// <summary> Indicates whether this generator always generates temporary values. Always returns <c>false</c>. </summary>
    public override bool GeneratesTemporaryValues => false;

    /// <summary> Creates a new instance of IdValueGenerator and initializes it with the provided identifier. </summary>
    /// <param name="id"> The initial identifier to use for generation. </param>
#pragma warning disable RCS1158 // Static member in generic type should use a type parameter
    internal static void SetGeneratorId(int id) => _generator = new IdGenerator(id);
#pragma warning restore RCS1158 // Static member in generic type should use a type parameter

    /// <summary> Generates the next identifier value. </summary>
    /// <param name="entry"> The <see cref="EntityEntry"/> instance. Not used in this implementation. </param>
    /// <returns> The generated identifier value. </returns>
    public override TId Next(EntityEntry entry) => Next();

    /// <summary> Generates the next identifier value. </summary>
    /// <returns> The generated identifier value. </returns>
    public static TId Next() => _generator is null
        ? throw new InvalidOperationException("The Value Generator must be initialized first")
        : (TId)Activator.CreateInstance(typeof(TId), _generator.CreateId())!;
}