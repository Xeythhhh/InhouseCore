using SharedKernel.Champions;

using Domain.Champions;

namespace Application.Champions;
public static class ChampionExtensions
{
    public static GetChampionDto ToGetChampionDto(this Champion champion) =>
        new(
            champion.Id,
            champion.Name.Value,
            champion.Role.Value);
}
