namespace SharedKernel.Contracts.v1.Champions;

public sealed record UpdateAugmentRequest(
    long AugmentId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor);
