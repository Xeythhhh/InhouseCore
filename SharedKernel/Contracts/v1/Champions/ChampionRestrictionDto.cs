namespace SharedKernel.Contracts.v1.Champions;

public sealed class ChampionRestrictionDto
{
    public ChampionRestrictionDto()
    {
        //if (!string.IsNullOrEmpty(Target))
        //{
        //    string[] targetValues = Target.Split(';');
        //    Name = targetValues[0];
        //    Identifier = targetValues[1];
        //}
    }

    public string RestrictionId { get; set; }
    public string Target { get; set; }
    public string Reason { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }
}
