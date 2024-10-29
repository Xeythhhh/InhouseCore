using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;

using WebApp.Champions.Restrictions.Dialogs;
using WebApp.Extensions;

namespace WebApp.Champions;

public partial class EditChampion
{
    private async Task AddRestriction() =>
        await AddRestrictionDialog.Show(DialogService, Id, Model.Augments)
            .TapError(Snackbar.NotifyErrors)
            .Tap(async () =>
            {
                Snackbar.Add("Restriction added", Severity.Success);
                await FetchChampion();
            });

    private async Task UpdateRestriction(ChampionRestrictionDto restriction) =>
        await UpdateRestrictionDialog.Show(DialogService, restriction, Model.Augments)
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
                Model.Restrictions.Remove(restriction);
                Snackbar.Add("Restriction removed", Severity.Success);
            });
}
