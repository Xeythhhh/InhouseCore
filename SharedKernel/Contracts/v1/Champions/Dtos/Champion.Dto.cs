namespace SharedKernel.Contracts.v1.Champions.Dtos;

public sealed class ChampionDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public bool HasRestrictions { get; set; }

    public List<ChampionAugmentDto> Augments { get; set; } = new();
    public List<ChampionRestrictionDto> Restrictions { get; set; } = new();

    public ChampionDto() { }

    public ChampionDto(
        string id,
        string name,
        string role,
        bool hasRestrictions,
        List<ChampionAugmentDto>? augments = null,
        List<ChampionRestrictionDto>? restrictions = null)
    {
        Id = id;
        Name = name;
        Role = role;
        HasRestrictions = hasRestrictions;
        Augments = augments ?? new();
        Restrictions = restrictions ?? new();
    }
}
