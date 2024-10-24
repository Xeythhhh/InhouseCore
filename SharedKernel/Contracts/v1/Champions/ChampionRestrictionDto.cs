namespace SharedKernel.Contracts.v1.Champions;

public sealed class ChampionRestrictionDto
{
    public string RestrictionId { get; set; }
    public string Reason { get; set; }
    public string AbilityName { get; set; }
    public string Identifier { get; set; }
    public string ColorHex { get; set; }
}
