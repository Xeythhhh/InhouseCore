using Domain.Primitives;

namespace Domain.Champions;

public class ChampionRestriction : EntityBase<ChampionRestriction.RestrictionId>
{
    /// <summary>Strongly-typed Id for <see cref="ChampionRestriction"/>.</summary>
    public sealed record RestrictionId(long Value) :
        EntityId<ChampionRestriction>(Value);

    public string DefaultKey { get; set; }
    public string Name { get; set; }
    public string Reason { get; set; }
}
