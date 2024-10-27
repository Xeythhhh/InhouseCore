using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Services;

namespace WebApp.Pages.Champions.Dialogs;

public partial class EditChampionDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    [Parameter] public string Id { get; set; }
    [Parameter] public string Name { get; set; }
    [Parameter] public string Role { get; set; }
    [Parameter] public List<ChampionAugmentDto> Augments { get; set; } = new();
    [Parameter] public List<ChampionRestrictionDto> Restrictions { get; set; } = new();

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IChampionService ChampionService { get; set; }

    protected override async void OnInitialized() => await FetchChampion();

    private async Task FetchChampion() =>
        await ChampionService.GetChampionByIdAsync(Id)
            .Tap(champion =>
            {
                Id = champion!.Id;
                Name = champion.Name;
                Role = champion.Role;
                Augments = champion.Augments;
                Restrictions = champion.Restrictions;

                StateHasChanged();
            });

    private void UpdateSplashArt() => Snackbar.Add("Not implemented", Severity.Error);
    private static string GetChampionStyle() => $"background-image: url('{GetChampionImageUrl()}'); background-size: cover; background-position: center; padding: 1rem;";
    private static string GetChampionImageUrl() => "https://storge.pic2.me/c/1360x800/655/61d9f40521ab03.12776299.jpg";
    private static string GetAugmentImageUrl() => "https://static.wikia.nocookie.net/battlerite_gamepedia_en/images/e/e2/Rain_Of_Arrows_icon_big.png";
    private static string GetAugmentStyle() => $@"
background-image: linear-gradient(to right, rgba(255, 255, 255, 0.7), rgba(255, 255, 255, 0)), url('{GetAugmentImageUrl()}');
background-size: 30%;
background-repeat: no-repeat;
background-position: left;
padding: 1rem;
display: flex; 
justify-content: center; 
align-items: center; 
height: 100%;";
}
