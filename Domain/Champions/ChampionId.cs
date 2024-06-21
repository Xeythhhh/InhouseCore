using Domain.Primitives;

namespace Domain.Champions;

/// <summary>Strongly-typed Id for <see cref="Champion"/>.</summary>
public sealed record ChampionId(long Value) :
    EntityId<Champion>(Value);
