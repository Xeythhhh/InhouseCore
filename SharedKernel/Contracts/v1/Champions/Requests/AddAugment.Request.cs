namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record AddAugmentRequest(
    string ChampionId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor);