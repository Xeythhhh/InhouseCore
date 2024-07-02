using Domain.Champions;
using SharedKernel.Contracts.Responses.Champions;

namespace Application.Champions;
public static class ChampionExtensions
{
    public static GetChampionResponse ToGetChampionDto(this Champion champion) =>
        new(
            champion.Id,
            champion.Name.Value,
            champion.Role.Value);
}
