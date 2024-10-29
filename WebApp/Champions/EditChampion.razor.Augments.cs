using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Champions.Augments.Dialogs;
using WebApp.Extensions;

namespace WebApp.Champions;

public partial class EditChampion
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
         await Result.OkIfAsync(DialogService.ShowMessageBox(
                "Delete Augment",
                $"Are you sure you want to delete '{augment}'?",
                yesText: "Delete",
                cancelText: "Cancel"), new Error("Prompt declined"))
            .Bind(async () => await ChampionService.RemoveAugmentAsync(Id, augment.AugmentId))
            .Tap(() =>
            {
                Model.Augments.Remove(augment);
                Snackbar.Add("Augment removed", Severity.Success);
            });
}
