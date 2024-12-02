using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Primitives.Result;

using WebApp.Champions.Restrictions.Abstract;
using WebApp.Champions.Restrictions.Models;
using WebApp.Extensions;

namespace WebApp.Champions.Restrictions.Dialogs;

public class AddRestrictionDialog : RestrictionDialogBase<AddRestrictionModel>
{
    protected override void OnInitialized()
    {
        Request = async () => await ChampionService.AddRestrictionAsync(Model);
        Title = "Add Restriction";
        SaveButtonLabel = "Add";
        base.OnInitialized();
    }

    public static async Task<Result> Show(IDialogService dialogService, long championId, List<ChampionAugmentDto> augments) =>
        await dialogService.Show<AddRestrictionDialog>("Add Restriction", new DialogParameters<AddRestrictionDialog>()
            {{ x => x.Model, new AddRestrictionModel(championId, augments)}})
            .DialogToResult();
}