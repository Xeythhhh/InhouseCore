namespace SharedKernel.Contracts.v1.Champions;

public sealed record UpdateRestrictionRequest(
    long RestrictionId,
    long AugmentId,
    long? AugmentId2,
    string Reason);
