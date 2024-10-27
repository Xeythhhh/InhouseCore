using System.Net.Http.Json;

using MudBlazor;

using SharedKernel.Contracts.v1;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace WebApp.Extensions;

public static class HttpResponseExtensions
{
    private static async Task<Result> HandleHttpErrorResponseCore(this HttpResponseMessage response, ISnackbar snackbar) =>
        await Result.OkIf(!response.IsSuccessStatusCode, new Error("Success Status Code"))
            .Bind<ErrorResponse?>(async () => await response.Content.ReadFromJsonAsync<ErrorResponse>())
            .Ensure(errorResponse => errorResponse?.Errors != null, "[HttpErrorResponseHandler] An unexpected error occurred.")
            .Map(errorResponse => errorResponse!.Errors)
            .Tap(snackbar.NotifyErrors);

    public static async Task<Result> HandleHttpErrorResponse(this HttpResponseMessage response, ISnackbar snackbar) =>
        response.IsSuccessStatusCode
            ? Result.Ok()
            : (await response.HandleHttpErrorResponseCore(snackbar)).IsSuccess
                ? Result.Fail("An Error occurred")
                : Result.Ok();
}