using static SharedKernel.Contracts.Responses.Champions.GetAllChampionsResponse;

namespace SharedKernel.Contracts.Responses.Champions;
public sealed record GetAllChampionsResponse(IEnumerable<ChampionDto> Champions)
{
    public sealed record ChampionDto(long Id, string Name, string Role);
}
