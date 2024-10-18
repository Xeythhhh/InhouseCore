namespace SharedKernel.Contracts.v1.Champions;
public sealed record GetAllChampionsResponse(IEnumerable<ChampionDto> Champions);
