using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;
using MudBlazor;

using SharedKernel.Contracts.v1.Champions;

using SharedKernel.Contracts.v1;

namespace WebApp.Pages.Champions;
public partial class CreateChampionDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    [Parameter] public string Name { get; set; }
    [Parameter] public string Role { get; set; }

    [Inject] private HttpClient HttpClient { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private async Task CreateChampion()
    {
        CreateChampionRequest request = new(Name, Role);
        try
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("champions", request);

            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Champion created successfully!", Severity.Success);
            }
            else
            {
                ErrorResponse? errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                if (errorResponse?.Errors != null)
                {
                    foreach (string error in errorResponse.Errors)
                    {
                        Snackbar.Add(error, Severity.Error);
                    }
                }
                else
                {
                    Snackbar.Add("An unexpected error occurred.", Severity.Error);
                }
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
        finally
        {
            MudDialog.Close();
        }
    }
}