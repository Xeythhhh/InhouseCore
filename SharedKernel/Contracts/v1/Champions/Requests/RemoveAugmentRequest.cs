namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record RemoveAugmentRequest(
    string ChampionId,
    string AugmentId);
