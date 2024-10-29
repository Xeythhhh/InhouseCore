using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Services;

namespace WebApp.Champions;

public partial class Champions
{
    private const string Address = "champions";
    public static string Location() => Address;

    [Inject] private IChampionService ChampionService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private List<ChampionDto> champions = new();

    protected override async Task OnInitializedAsync() => await FetchChampions();

    private async Task FetchChampions() =>
        await ChampionService.GetChampionsAsync()
            .Tap(response =>
            {
                champions = response.Champions.ToList();
                StateHasChanged();
            });

    private async Task Create() =>
        await CreateChampionDialog.Show(DialogService)
            .Tap(FetchChampions);

    private void Edit(string id) => NavigationManager.NavigateTo(EditChampion.Location(id));

    private async Task Delete(string id) =>
        await Result.OkIfAsync(DialogService.ShowMessageBox(
                "Delete Champion",
                "Are you sure you want to delete this champion?",
                yesText: "Delete",
                cancelText: "Cancel"), new Error("Prompt declined"))
            .Bind(async () => await ChampionService.DeleteChampionAsync(id))
            .Tap(FetchChampions);

    private static string GetChampionStyle(ChampionDto champion) => $@"
background-image: linear-gradient(to right, rgba(255, 255, 255, 0.7), rgba(255, 255, 255, 0)), url('{GetChampionImageUrl(champion)}');
background-size: cover;
background-position: center;
padding: 1rem;";

#pragma warning disable RCS1163 // Unused parameter
#pragma warning disable IDE0060 // Remove unused parameter
    private static string GetChampionImageUrl(ChampionDto champion) =>
        "https://storge.pic2.me/c/1360x800/655/61d9f40521ab03.12776299.jpg";
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore RCS1163 // Unused parameter
}
