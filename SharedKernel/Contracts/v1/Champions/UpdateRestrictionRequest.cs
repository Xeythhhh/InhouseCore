namespace SharedKernel.Contracts.v1.Champions;

public sealed record UpdateRestrictionRequest(
    string RestrictionId,
    string AbilityName,
    string AbilityIdentifier,
    string Color,
    string Reason);
