using Domain.Primitives;
using Domain.Primitives.Result;

using IdGen;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Identifiers;

/// <summary> Value generator for generating unique identifier values using IdGen. </summary>
internal sealed class IdValueGenerator :
    ValueGenerator<long>
{
    private static IdGenerator? _generator;

    /// <summary> Indicates whether this generator always generates temporary values. Always returns <c>false</c>. </summary>
    public override bool GeneratesTemporaryValues => false;

    private IdValueGenerator() { }

    /// <summary> Creates a new instance of IdValueGenerator and initializes it with the provided identifier. </summary>
    /// <param name="id"> The initial identifier to use for generation. </param>
    /// <returns> A <see cref="Result"/> indicating success or failure. </returns>
    internal static Result Register(int id)
    {
        if (_generator is not null)
            return Result.Failure(new Error("IdValueGenerator.Create", "IdValueGenerator already registered"));

        _generator = new IdGenerator(id);
        return Result.Success();
    }

    /// <summary> Generates the next identifier value. </summary>
    /// <param name="entry"> The <see cref="EntityEntry"/> instance. Not used in this implementation. </param>
    /// <returns> The generated identifier value. </returns>
    public override long Next(EntityEntry entry) => _generator is null
        ? throw new InvalidOperationException("The Value Generator must be initialized first")
        : _generator.CreateId();
}
