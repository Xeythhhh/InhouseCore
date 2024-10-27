using SharedKernel.Contracts.v1.Champions.Dtos;

namespace SharedKernel.Contracts.v1.Champions.Responses;
public sealed record GetAllChampionsResponse(IEnumerable<ChampionDto> Champions);
