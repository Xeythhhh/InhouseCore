using Domain.Primitives;

namespace Domain.ReferenceData;
public sealed class Game :
    EntityBase<Game.GameId>
{
    /// <summary>Strongly-typed Id for <see cref="Game"/>.</summary>
    public sealed record GameId(long Value) :
        EntityId<Game>(Value);

    public string Name { get; set; }
    public string[] Roles { get; set; }
    public string[] Augments { get; set; }
    public string[] AugmentColors { get; set; }
}
