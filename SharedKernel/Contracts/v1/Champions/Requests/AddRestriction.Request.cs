namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record AddRestrictionRequest(
    string ChampionId,
    string AugmentId,
    string? AugmentId2,
    string Reason);