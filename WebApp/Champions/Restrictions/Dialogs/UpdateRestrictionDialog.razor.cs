using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Primitives.Result;

using WebApp.Champions.Restrictions.Abstract;
using WebApp.Champions.Restrictions.Models;
using WebApp.Extensions;

namespace WebApp.Champions.Restrictions.Dialogs;

public class UpdateRestrictionDialog : RestrictionDialogBase<UpdateRestrictionModel>
{
    protected override void OnInitialized()
    {
        Request = async () => await ChampionService.UpdateRestrictionAsync(Model);
        Title = "Update Restriction";
        SaveButtonLabel = "Update";
        base.OnInitialized();
    }

    public static async Task<Result> Show(IDialogService dialogService, ChampionRestrictionDto restriction, List<ChampionAugmentDto> augments) =>
        await dialogService.Show<UpdateRestrictionDialog>("Update Restriction", new DialogParameters<UpdateRestrictionDialog>()
            {{ x => x.Model, new UpdateRestrictionModel(
                restriction.RestrictionId,
                augments,
                augments.Find(a => a.AugmentId == restriction.RestrictedAugmentId),
                augments.Find(a => a.AugmentId == restriction.RestrictedComboAugmentId),
                restriction.Reason)}})
            .DialogToResult();
}