using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Services;

namespace WebApp.Champions;

public partial class EditChampion
{
    [Parameter] public string Id { get; set; }
    private const string Address = "edit-champion";
    public static string Location(string id) => $"{Address}/{id}";

    public ChampionDto Model { get; set; }

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ChampionService ChampionService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    protected override async void OnInitialized() => await FetchChampion();

    private async Task FetchChampion() =>
        await ChampionService.GetChampionByIdAsync(Id)
            .Tap(champion =>
            {
                Model = champion;
                StateHasChanged();
            });

    private void NavigateToChampionList() => NavigationManager.NavigateTo(Champions.Location());

    private void UpdateSplashArt() => Snackbar.Add("Not implemented", Severity.Error);
    private static string ChampionImageUrl => "https://storge.pic2.me/c/1360x800/655/61d9f40521ab03.12776299.jpg";
}
