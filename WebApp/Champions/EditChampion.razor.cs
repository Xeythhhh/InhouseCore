using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Services;

namespace WebApp.Champions;

public partial class EditChampion
{
    [Parameter] public string ChampionId { get; set; }
    public long Id => long.Parse(ChampionId);
    private const string Address = "edit-champion";
    public static string Location(long id) => $"{Address}/{id}";

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
}
