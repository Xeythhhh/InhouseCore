using MudBlazor;

using SharedKernel.Primitives.Result;

namespace WebApp.Extensions;

public static class ResultExtensions
{
    /// <summary> Awaits the dialog result and returns Ok if the dialog is successful, Fail otherwise.</summary>
    public static async Task<Result> DialogToResult(this IDialogReference dialog)
    {
        DialogResult? result = await dialog.Result;

        return result?.Canceled != false
            ? Result.Fail("Dialog Failed")
            : Result.Ok();
    }
}
