using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Services;

namespace WebApp.Pages.Champions.Dialogs;
public partial class CreateChampionDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    [Parameter] public string Name { get; set; }
    [Parameter] public string Role { get; set; }

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IChampionService ChampionService { get; set; }

    private async Task CreateChampion() =>
        await ChampionService.CreateChampionAsync(Name, Role)
            .Tap(() =>
            {
                Snackbar.Add($"Champion '{Name}' created successfully!", Severity.Success);
                MudDialog.Close();
            });

    public static async Task<Result> Show(IDialogService dialogService) =>
        await dialogService.Show<CreateChampionDialog>("Create Champion")
            .DialogToResult();
}