namespace SharedKernel.Contracts.v1.Champions;

public sealed record AddRestrictionRequest(
    string ChampionId,
    string Target,
    string Reason);
