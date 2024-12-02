namespace SharedKernel.Contracts.v1.Champions.Dtos;

public sealed class ChampionAugmentDto
{
    public long AugmentId { get; set; }
    public string AugmentName { get; set; }
    public string AugmentTarget { get; set; }
    public string AugmentColor { get; set; }
    public string AugmentIcon { get; set; }

    public ChampionAugmentDto() { }

    public ChampionAugmentDto(
        long augmentId,
        string augmentName,
        string augmentTarget,
        string augmentColor,
        string augmentIcon)
    {
        AugmentId = augmentId;
        AugmentName = augmentName;
        AugmentTarget = augmentTarget;
        AugmentColor = augmentColor;
        AugmentIcon = augmentIcon;
    }

    public override string ToString() => $"{AugmentName} ({AugmentTarget})";
}