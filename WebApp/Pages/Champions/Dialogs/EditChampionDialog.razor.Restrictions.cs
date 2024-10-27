using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Extensions;
using WebApp.Pages.Champions.Dialogs.Restrictions;

namespace WebApp.Pages.Champions.Dialogs;

public partial class EditChampionDialog
{
    private async Task AddRestriction() =>
        await AddRestrictionDialog.Show(DialogService, Id, Augments)
            .TapError(Snackbar.NotifyErrors)
            .Tap(async () =>
            {
                Snackbar.Add("Restriction added", Severity.Success);
                await FetchChampion();
            });

    private async Task UpdateRestriction(ChampionRestrictionDto restriction) =>
        await UpdateRestrictionDialog.Show(DialogService, restriction, Augments)
            .TapError(Snackbar.NotifyErrors)
            .Tap(async () =>
            {
                Snackbar.Add("Restriction updated", Severity.Success);
                await FetchChampion();
            });

    private async Task RemoveRestriction(ChampionRestrictionDto restriction) =>
        await ChampionService.RemoveRestrictionAsync(Id, restriction.RestrictionId)
            .Tap(() =>
            {
                Restrictions.Remove(restriction);
                Snackbar.Add("Restriction removed", Severity.Success);
            });
}
