namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record AddRestrictionRequest(
    long ChampionId,
    long AugmentId,
    long? AugmentId2,
    string Reason);