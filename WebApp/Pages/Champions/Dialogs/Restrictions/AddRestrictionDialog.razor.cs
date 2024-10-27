using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Pages.Champions.Dialogs.Restrictions.Abstract;

namespace WebApp.Pages.Champions.Dialogs.Restrictions;

public class AddRestrictionDialog : RestrictionDialogBase<AddRestrictionModel>
{
    protected override void OnInitialized()
    {
        Request = ChampionService.AddRestrictionAsync(Model);
        Title = "Add Restriction";
        SaveButtonLabel = "Add";
        base.OnInitialized();
    }

    public static async Task<Result> Show(IDialogService dialogService, string championId, List<ChampionAugmentDto> augments) =>
        await dialogService.Show<AddRestrictionDialog>("Add Restriction", new DialogParameters<AddRestrictionDialog>()
            {{ x => x.Model, new AddRestrictionModel(championId, augments)}})
            .DialogToResult();
}