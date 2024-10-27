using MudBlazor;

using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Pages.Champions.Dialogs.Augment.Abstract;

namespace WebApp.Pages.Champions.Dialogs.Augment;

public class AddAugmentDialog : AugmentDialogBase<AddAugmentModel>
{
    protected override void OnInitialized()
    {
        Request = ChampionService.AddAugmentAsync(Model);
        Title = "Add Augment";
        SaveButtonLabel = "Add";
        base.OnInitialized();
    }

    public static async Task<Result> Show(IDialogService dialogService, string championId) =>
        await dialogService.Show<AddAugmentDialog>("Add Augment", new DialogParameters<AddAugmentDialog>()
            {{ x => x.Model, new AddAugmentModel(championId)}})
            .DialogToResult();
}