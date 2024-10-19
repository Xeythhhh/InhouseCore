using Domain.Primitives;

namespace Domain.Champions;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class ChampionRestriction : EntityBase<ChampionRestriction.RestrictionId>
{
    /// <summary>Strongly-typed Id for <see cref="ChampionRestriction"/>.</summary>
    public sealed record RestrictionId(long Value) :
        EntityId<ChampionRestriction>(Value);

    public string DefaultKey { get; set; }
    public string Name { get; set; }
    public string Reason { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.