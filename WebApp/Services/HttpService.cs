using MudBlazor;

using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;

namespace WebApp.Services;

public abstract class HttpService(ISnackbar snackbar)
{
    /// <summary> Executes an HTTP request and processes the result.</summary>
    /// <param name="task">The task representing the HTTP request to execute.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation, with errors displayed via snackbar if failed.</returns>
    public async Task<Result> ApiRequest(Task<HttpResponseMessage> task) =>
        await Result.Try(task)
            .Ensure(response => response is not null, new Error("HTTP Response was null"))
            .Map(response => response!)
            .TapError(snackbar.NotifyErrors)
            .Bind(response => response.HandleHttpErrorResponse(snackbar));

    /// <summary> Executes an HTTP request for nullable results and processes the outcome.</summary>
    /// <typeparam name="T">The type of the expected response.</typeparam>
    /// <param name="task">The task representing the HTTP request to execute.</param>
    /// <returns>A <see cref="Result{T}"/> containing the response if successful, or error information if failed.</returns>
    public async Task<Result<T>> ApiRequestNullable<T>(Task<T?> task) =>
        await Result.Try(task)
            .Ensure(response => response is not null, new Error("HTTP Response was null"))
            .Map(response => response!)
            .TapError(snackbar.NotifyErrors);
}
