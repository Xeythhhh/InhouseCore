using Domain.Primitives;
using Domain.ReferenceData;

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

    public Game.GameId GameId { get; private set; }

    /// <summary>Gets or sets the name of the champion.</summary>
    public ChampionName Name { get; private set; }
    /// <summary>Gets the role of the champion.</summary>
    public ChampionRole Role { get; private set; }
    /// <summary>Gets or sets the avatar for the champion.</summary>
    public ChampionAvatar Avatar { get; private set; }
    /// <summary>Gets the champion's augments.</summary>
    public List<Augment> Augments { get; set; } = new List<Augment>();
    /// <summary>Gets the champion's restrictions.</summary>
    public List<Restriction> Restrictions { get; set; } = new List<Restriction>();
    /// <summary>Gets the HasRestrictions flag of the champion.</summary>
    /// <remarks>It is used to optimize queries in the read model.</remarks>
    public bool HasRestrictions { get; set; }

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    private Champion() { }

    /// <summary>Creates a new instance of a <see cref="Champion"/> with the specified name, role, and avatar.</summary>
    /// <param name="name">The name of the champion.</param>
    /// <param name="role">The role assigned to the champion.</param>
    /// <param name="portraitUri">The URI for the portrait image.</param>
    /// <param name="portraitWideUri">The URI for the wide image.</param>
    /// <returns>A <see cref="Result{Champion}"/> containing the created champion instance if successful, otherwise a failure result.</returns>
    public static Result<Champion> Create(string name, string role, string portraitUri, string portraitWideUri) =>
        Result.Try(() => CreateInternal(
                (ChampionName)name,
                (ChampionRole)role,
                (ChampionAvatar)(portraitUri, portraitWideUri)
            ),
            ex => new CreateChampionError().CausedBy(ex));

    public void SetGame(Game game) => GameId = game.Id;

    private static Champion CreateInternal(ChampionName name, ChampionRole role, ChampionAvatar avatar) => new()
    {
        Name = name,
        Role = role,
        Avatar = avatar
    };

    /// <summary>Provides error messages for <see cref="Champion"/>.</summary>
    public class CreateChampionError() : Error("An error occurred creating a champion instance.");
}
