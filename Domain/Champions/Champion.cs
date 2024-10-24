using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

/// <summary>Represents a champion entity.</summary>
public sealed partial class Champion :
    AggregateRoot<Champion.ChampionId>
{
    /// <summary>Strongly-typed Id for <see cref="Champion"/>.</summary>
    public sealed record ChampionId(long Value) :
        EntityId<Champion>(Value);

    /// <summary>Gets or sets the name of the champion.</summary>
    public ChampionName Name { get; private set; }
    /// <summary>Gets the role of the champion.</summary>
    public ChampionRole Role { get; private set; }
    /// <summary>Gets the restrictions of the champion.</summary>
    public List<Restriction> Restrictions { get; set; } = new List<Restriction>();
    /// <summary>Gets the HasRestrictions flag of the champion.</summary>
    /// <remarks>It is used to optimize queries in the read model.</remarks>
    public bool HasRestrictions { get; set; }

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    private Champion() { }

    /// <summary>Creates a new instance of a <see cref="Champion"/> with the specified name and role.</summary>
    /// <param name="name">The name of the champion.</param>
    /// <param name="role">The role assigned to the champion.</param>
    /// <returns>A <see cref="Result{Champion}"/> containing the created champion instance if successful, otherwise a failure result.</returns>
    public static Result<Champion> Create(string name, string role) =>
        Result.Try(() => CreateInternal((ChampionName)name, (ChampionRole)role),
            ex => new CreateChampionError().CausedBy(ex));

    private static Champion CreateInternal(ChampionName name, ChampionRole role) => new()
    {
        Name = name,
        Role = role
    };

    /// <summary>Provides error messages for <see cref="Champion"/>.</summary>
    public class CreateChampionError() : Error("An error occurred creating a champion instance.");
}
