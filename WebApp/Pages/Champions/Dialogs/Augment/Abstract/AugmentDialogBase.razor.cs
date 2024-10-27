using System.Configuration;

using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Services;

namespace WebApp.Pages.Champions.Dialogs.Augment.Abstract;

public abstract partial class AugmentDialogBase<TModel> : ComponentBase
    where TModel : AugmentModelBase
{
    [Inject] protected IChampionService ChampionService { get; set; }
    [Inject] protected IConfiguration Configuration { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected HttpClient HttpClient { get; set; }
    [Inject] protected AugmentModelBase.Validator AugmentValidator { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    [Parameter] public TModel Model { get; set; }

    protected string Title { get; set; }
    protected string SaveButtonLabel { get; set; }
    protected string IconUrl { get; set; } = "https://static.wikia.nocookie.net/battlerite_gamepedia_en/images/e/e2/Rain_Of_Arrows_icon_big.png";
    protected Task<Result> Request { get; set; }
    protected IEnumerable<MudColor> AugmentColorPalette { get; set; }

    protected MudForm Form;

    protected override void OnInitialized()
    {
        string[] palette = Configuration.GetSection("ChampionAugments:ColorPalette").Get<string[]>()
            ?? throw new ConfigurationErrorsException("ChampionAugments:ColorPalette not found in configuration.");
        AugmentColorPalette = palette.Select(color => new MudColor(color)).ToArray();
    }

    protected async Task Save() => await Request
        .TapError(Snackbar.NotifyErrors)
        .Tap(MudDialog.Close);

    protected void UploadIcon() => Snackbar.Add("Not implemented", Severity.Warning);

    protected string AugmentBorderStyle => $"border-color: {Model.AugmentColor}!important;";
}