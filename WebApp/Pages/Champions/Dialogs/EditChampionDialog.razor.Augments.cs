using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Extensions;
using WebApp.Pages.Champions.Dialogs.Augment;

namespace WebApp.Pages.Champions.Dialogs;

public partial class EditChampionDialog
{
    private async Task AddAugment() =>
        await AddAugmentDialog.Show(DialogService, Id)
            .TapError(Snackbar.NotifyErrors)
            .Tap(async () =>
            {
                Snackbar.Add("Augment added", Severity.Success);
                await FetchChampion();
            });

    private async Task UpdateAugment(ChampionAugmentDto augment) =>
        await UpdateAugmentDialog.Show(DialogService, Id, augment)
            .TapError(Snackbar.NotifyErrors)
            .Tap(async () =>
            {
                Snackbar.Add("Augment updated", Severity.Success);
                await FetchChampion();
            });

    private async Task RemoveAugment(ChampionAugmentDto augment) =>
        await ChampionService.RemoveAugmentAsync(Id, augment.AugmentId)
            .Tap(() =>
            {
                Augments.Remove(augment);
                Snackbar.Add("Augment removed", Severity.Success);
            });
}
