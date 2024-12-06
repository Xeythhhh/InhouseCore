namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record AddAugmentRequest(
    long ChampionId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor,
    string AugmentIcon);