using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Primitives.Result;

using WebApp.Champions.Augments.Abstract;
using WebApp.Champions.Augments.Models;
using WebApp.Extensions;

namespace WebApp.Champions.Augments.Dialogs;

public class UpdateAugmentDialog : AugmentDialogBase<UpdateAugmentModel>
{
    protected override void OnInitialized()
    {
        Request = async () => await ChampionService.UpdateAugmentAsync(Model);
        Title = "Update Augment";
        SaveButtonLabel = "Update";
        base.OnInitialized();
    }

    public static async Task<Result> Show(IDialogService dialogService, string championId, ChampionAugmentDto augment) =>
        await dialogService.Show<UpdateAugmentDialog>("Update Augment", new DialogParameters<UpdateAugmentDialog>()
            {{ x => x.Model, new UpdateAugmentModel(
                championId,
                augment.AugmentId,
                augment.AugmentName,
                augment.AugmentTarget,
                augment.AugmentColor)}})
            .DialogToResult();
}