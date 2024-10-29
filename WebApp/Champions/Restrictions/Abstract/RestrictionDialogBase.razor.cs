using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;
using WebApp.Services;

namespace WebApp.Champions.Restrictions.Abstract;

public abstract partial class RestrictionDialogBase<TModel> : ComponentBase
    where TModel : RestrictionModelBase
{
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IChampionService ChampionService { get; set; }
    [Inject] protected HttpClient HttpClient { get; set; }
    [Inject] protected RestrictionModelBase.Validator RestrictionValidator { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    [Parameter] public TModel Model { get; set; }

    protected string Title { get; set; }
    protected string SaveButtonLabel { get; set; }
    protected Func<Task<Result>> Request { get; set; }

    protected MudForm Form;

    protected async Task Save() => await Request()
        .TapError(Snackbar.NotifyErrors)
        .Tap(MudDialog.Close);

    protected void UploadIcon() => Snackbar.Add("Not implemented", Severity.Warning);
}