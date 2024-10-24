namespace SharedKernel.Contracts.v1.Champions;

public sealed record AddRestrictionRequest(
    string ChampionId,
    string AbilityName,
    string AbilityIdentifier,
    string Color,
    string Reason);
