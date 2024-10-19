using Domain.Champions.ValueObjects;
using Domain.Primitives;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Domain.Champions;

/// <summary>Represents a champion entity.</summary>
public sealed class Champion :
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
    public List<ChampionRestriction> Restrictions { get; set; }
    public bool HasRestrictions => Restrictions.Count != 0;

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    private Champion() { }

    /// <summary>Creates a new instance of the <see cref="Champion"/> class.</summary>
    /// <param name="name">The name of the champion.</param>
    /// <param name="role">The role of the champion.</param>
    /// <returns>A result containing the created <see cref="Champion"/> instance if successful, otherwise a failure result.</returns>
    public static Result<Champion> Create(string name, string role, IEnumerable<ChampionRestrictionDto>? restrictions = null)
    {
        try
        {
            return CreateInternal((ChampionName)name, (ChampionRole)role, restrictions);
        }
        catch (Exception exception)
        {
            return Result.Fail(new CreateChampionError().CausedBy(exception));
        }
    }

    private static Result<Champion> CreateInternal(ChampionName name, ChampionRole role, IEnumerable<ChampionRestrictionDto>? restrictions)
    {
        Champion instance = new()
        {
            Name = name,
            Role = role,
            Restrictions = restrictions?.Select(r => new ChampionRestriction()
            {
                DefaultKey = r.DefaultKey,
                Name = r.Name,
                Reason = r.Reason
            }).ToList() ?? new List<ChampionRestriction>()
        };

        return Result.Ok(instance);
    }

    /// <summary>Provides error messages for <see cref="Champion"/>.</summary>
    public class CreateChampionError() : Error("An error occurred creating a champion instance.");
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
