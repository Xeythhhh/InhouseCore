namespace SharedKernel.Contracts.v1.Champions.Responses;

public sealed record GetAvailableChampionAugmentTargetsAndColorsResponse(IEnumerable<string> AugmentTargets, IEnumerable<string> AugmentColors);
