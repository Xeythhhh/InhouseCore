namespace SharedKernel.Contracts.v1.Champions;

public sealed class ChampionRestrictionDto()
{
    public long? Id { get; set; }
    public string DefaultKey { get; set; }
    public string Name { get; set; }
    public string Reason { get; set; }
}
