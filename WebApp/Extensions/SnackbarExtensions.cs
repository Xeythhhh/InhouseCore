using MudBlazor;

using SharedKernel.Primitives.Reasons;

namespace WebApp.Extensions;

public static class SnackbarExtensions
{
    public static void NotifyErrors(this ISnackbar? snackbar, IEnumerable<IError> errors) =>
        snackbar.NotifyErrors(errors.Select(e => e.Message));

    public static void NotifyErrors(this ISnackbar? snackbar, IEnumerable<string> errorMessages)
    {
        if (snackbar is null) return;

        foreach (string errorMessage in errorMessages)
            snackbar.Add(errorMessage, Severity.Error);
    }
}
