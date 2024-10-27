namespace SharedKernel.Contracts.v1.Champions;

public sealed record UpdateAugmentRequest(
    string AugmentId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor);
