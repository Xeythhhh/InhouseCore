using Microsoft.AspNetCore.Components;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Games;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Services;

namespace WebApp.Champions;

public partial class Champions
{
    private const string Address = "champions";
    public static string Location() => Address;

    [Inject] private ChampionService ChampionService { get; set; }
    [Inject] private GameService GameService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private List<GameDto> games = new();
    private List<ChampionDto> champions = new();
    private GameDto selectedGame;

    protected override async Task OnInitializedAsync() =>
         await GameService.GetGamesAsync()
            .Tap(async response =>
            {
                games = [new GameDto(0, "All"), .. response.Games.ToList()];
                selectedGame = games[0];
                await FetchChampions();
            });

    private async Task FetchChampions() =>
        await ChampionService.GetChampionsAsync(selectedGame.Id)
            .Tap(response =>
            {
                champions = response.Champions.ToList();
                StateHasChanged();
            });

    private void Edit(long id) => NavigationManager.NavigateTo(EditChampion.Location(id));
}
