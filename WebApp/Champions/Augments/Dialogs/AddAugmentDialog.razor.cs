using MudBlazor;

using SharedKernel.Primitives.Result;

using WebApp.Champions.Augments.Abstract;
using WebApp.Champions.Augments.Models;
using WebApp.Extensions;

namespace WebApp.Champions.Augments.Dialogs;

public class AddAugmentDialog : AugmentDialogBase<AddAugmentModel>
{
    protected override void OnInitialized()
    {
        Request = async () => await ChampionService.AddAugmentAsync(Model);
        Title = "Add Augment";
        SaveButtonLabel = "Add";
        base.OnInitialized();
    }

    public static async Task<Result> Show(IDialogService dialogService, string championId) =>
        await dialogService.Show<AddAugmentDialog>("Add Augment", new DialogParameters<AddAugmentDialog>()
            {{ x => x.Model, new AddAugmentModel(championId)}})
            .DialogToResult();
}