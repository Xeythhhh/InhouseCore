namespace SharedKernel.Contracts.v1.Champions.Responses;

public sealed record GetChampionAugmentNamesResponse(IReadOnlyCollection<string> AugmentNames);
