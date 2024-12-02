namespace SharedKernel.Contracts.v1.Champions.Dtos;

public sealed class ChampionDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    /// <summary>
    /// Format: $"{<see cref="Portrait"/>};{<see cref="PortraitWide"/>}"
    /// </summary>
    public string Avatar { get; set; }
    private string[] AvatarValues => Avatar.Split(';');
    public string Portrait => AvatarValues[0];
    public string PortraitWide => AvatarValues[1];
    public bool HasRestrictions { get; set; }

    public List<ChampionAugmentDto> Augments { get; set; } = new();
    public List<ChampionRestrictionDto> Restrictions { get; set; } = new();

    public ChampionDto() { }

    public ChampionDto(
        long id,
        string name,
        string role,
        string avatar,
        bool hasRestrictions,
        List<ChampionAugmentDto>? augments = null,
        List<ChampionRestrictionDto>? restrictions = null)
    {
        Id = id;
        Name = name;
        Role = role;
        Avatar = avatar;
        HasRestrictions = hasRestrictions;
        Augments = augments ?? new();
        Restrictions = restrictions ?? new();
    }
}
