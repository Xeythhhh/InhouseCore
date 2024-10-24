namespace SharedKernel.Contracts.v1.Champions;

public sealed class ChampionDto()
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public bool HasRestrictions { get; set; }

    public List<ChampionRestrictionDto> Restrictions { get; set; } = new List<ChampionRestrictionDto>();
}
