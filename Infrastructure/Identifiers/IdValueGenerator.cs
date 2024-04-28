using CSharpFunctionalExtensions;

using IdGen;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Identifiers;

internal sealed class IdValueGenerator
    : ValueGenerator<long>
{
    internal static Result Register(int id)
    {
        if (_generator is not null) return Result.Failure("IdValueGenerator already registered");

        _generator = new IdGenerator(id);
        return Result.Success();
    }

    internal IdValueGenerator() { }

    private static IdGenerator? _generator;

    public override bool GeneratesTemporaryValues => false;

    public override long Next(EntityEntry entry) => _generator is null
        ? throw new InvalidOperationException("The Value Generator must be initialized first")
        : _generator.CreateId();
}