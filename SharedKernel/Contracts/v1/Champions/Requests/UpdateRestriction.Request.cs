namespace SharedKernel.Contracts.v1.Champions;

public sealed record UpdateRestrictionRequest(
    string RestrictionId,
    string AugmentId,
    string? AugmentId2,
    string Reason);
