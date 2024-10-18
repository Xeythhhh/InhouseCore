namespace SharedKernel.Contracts.v1.Champions;

public sealed class ChampionDto()
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }

    public List<ChampionRestrictionDto>? Restrictions { get; set; }
}
