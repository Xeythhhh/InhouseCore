namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record RemoveAugmentRequest(
    long ChampionId,
    long AugmentId);
