namespace SharedKernel.Contracts.v1.Champions.Dtos;

public sealed class ChampionAugmentDto
{
    public string AugmentId { get; set; }
    public string AugmentName { get; set; }
    public string AugmentTarget { get; set; }
    public string AugmentColor { get; set; }

    public ChampionAugmentDto() { }

    public ChampionAugmentDto(
        string augmentId,
        string augmentName,
        string augmentTarget,
        string augmentColor)
    {
        AugmentId = augmentId;
        AugmentName = augmentName;
        AugmentTarget = augmentTarget;
        AugmentColor = augmentColor;
    }

    public override string ToString() => $"{AugmentName} ({AugmentTarget})";
}