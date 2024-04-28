using CSharpFunctionalExtensions;

using IdGen;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Identifiers;

internal sealed class IdValueGenerator
    : ValueGenerator<long>
{
    internal static Result RegisterGenerator(IdValueGeneratorOptions options)
    {
        if (options is null)
            return Result.Failure("IdValueGenerator configuration not found");

        _generatorId = options.GeneratorId;
        return Result.Success();
    }

    private IdValueGenerator() { }

    private static int? _generatorId;

    public override bool GeneratesTemporaryValues => false;

    public override long Next(EntityEntry entry) => _generatorId is null
        ? throw new InvalidOperationException("The Value Generator must be initialized first")
        : new IdGenerator(_generatorId.Value).CreateId();
}