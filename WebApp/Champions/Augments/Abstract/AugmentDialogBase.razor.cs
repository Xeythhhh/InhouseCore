using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Services;

namespace WebApp.Champions.Augments.Abstract;

public abstract partial class AugmentDialogBase<TModel> : ComponentBase
    where TModel : AugmentModelBase
{
    [Inject] protected ChampionService ChampionService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected AugmentModelBase.Validator AugmentValidator { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    [Parameter] public TModel Model { get; set; }

    protected string Title { get; set; }
    protected string SaveButtonLabel { get; set; }
    protected string IconUrl { get; set; } = "https://static.wikia.nocookie.net/battlerite_gamepedia_en/images/e/e2/Rain_Of_Arrows_icon_big.png";
    protected Func<Task<Result>> Request { get; set; }
    protected IEnumerable<(string, string)> AugmentColorPalette { get; set; }
    protected IEnumerable<string> AugmentTargetOptions { get; set; }

    protected MudForm Form;

    protected override async void OnInitialized() =>
        await ChampionService.GetAvailableAugmentTargetsAndColorsAsync(Model.ChampionId)
            .Tap(response =>
            {
                AugmentColorPalette = response.AugmentColors.Select(descriptor =>
                {
                    string[] values = descriptor.Split("|");
                    return (values[0], values[1]);
                }).ToArray();
                AugmentTargetOptions = response.AugmentTargets.ToList();
                StateHasChanged();
            });

    protected async Task Save() => await Request()
        .TapError(Snackbar.NotifyErrors)
        .Tap(MudDialog.Close);

    protected void UploadIcon() => Snackbar.Add("Not implemented", Severity.Warning);

    protected string AugmentBorderStyle => $"border-color: {Model.AugmentColor}!important;";
}