using Domain.Champions.ValueObjects;
using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

/// <summary>Represents a champion entity.</summary>
public sealed partial class Champion :
    AggregateRoot<ChampionId>
{
    /// <summary>Gets or sets the name of the champion.</summary>
    public ChampionName Name { get; private set; }
    /// <summary>Gets the role of the champion.</summary>
    public ChampionRole Role { get; private set; }

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Champion() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Creates a new instance of the <see cref="Champion"/> class.</summary>
    /// <param name="name">The name of the champion.</param>
    /// <param name="role">The role of the champion.</param>
    /// <returns>A result containing the created <see cref="Champion"/> instance if successful, otherwise a failure result.</returns>
    public static Result<Champion> Create(string name, string role)
    {
        try
        {
            return CreateInternal((ChampionName)name, (ChampionRole)role);
        }
        catch (Exception exception)
        {
            return Result.Fail(new CreateChampionError().CausedBy(exception));
        }
    }

    private static Result<Champion> CreateInternal(ChampionName name, ChampionRole role)
    {
        Champion instance = new()
        {
            Name = name,
            Role = role
        };

        return Result.Ok(instance);
    }

    /// <summary>Provides error messages for <see cref="Champion"/>.</summary>
    public class CreateChampionError() : Error("An error occurred creating a champion instance.");
}
