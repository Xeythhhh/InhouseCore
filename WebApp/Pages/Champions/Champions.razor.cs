using System.Net.Http.Json;

using MudBlazor;

using SharedKernel.Contracts.v1;
using SharedKernel.Contracts.v1.Champions;

namespace WebApp.Pages.Champions;
public partial class Champions
{
    private IEnumerable<ChampionDto>? champions;

    protected override async Task OnInitializedAsync() => await FetchChampions();

    private async Task FetchChampions()
    {
        try
        {
            GetAllChampionsResponse? apiResponse = await HttpClient.GetFromJsonAsync<GetAllChampionsResponse>("champions");
            if (apiResponse != null)
            {
                champions = apiResponse.Champions;
            }
            else
            {
                champions = new List<ChampionDto>();
                Snackbar.Add("Failed to fetch champions.", Severity.Error);
            }

            StateHasChanged();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred while fetching champions: {ex.Message}", Severity.Error);
        }
    }

    private async Task CreateChampion()
    {
        DialogResult? dialogResult = await DialogService.Show<CreateChampionDialog>("Create Champion").Result;

#pragma warning disable RCS1146 // Use conditional access
        if (dialogResult is not null && !dialogResult.Canceled)
        {
            await FetchChampions();
        }
#pragma warning restore RCS1146 // Use conditional access
    }

    private async void DeleteChampion(string id)
    {
        if (await DialogService.ShowMessageBox(
            "Delete Champion",
            "Are you sure you want to delete this champion?",
            yesText: "Delete", cancelText: "Cancel") == true)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.DeleteAsync($"champions/{id}");

                if (response.IsSuccessStatusCode)
                {
                    await FetchChampions();
                    Snackbar.Add("Champion deleted successfully.", Severity.Success);
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
        }
    }

    private async void EditChampion(string id)
    {
        try
        {
            ChampionDto? champion = await HttpClient.GetFromJsonAsync<ChampionDto>($"champions/{id}");

            if (champion is null)
            {
                Snackbar.Add("Champion not found", Severity.Error);
                return;
            }

            DialogParameters<EditChampionDialog> dialogParameters = new()
            {
                { x => x.Id, champion.Id },
                { x => x.Name, champion.Name },
                { x => x.Role, champion.Role },
                { x => x.Restrictions, champion.Restrictions }
            };

            DialogOptions dialogOptions = new()
            {
                FullScreen = true
            };

            DialogResult? dialogResult = await DialogService.Show<EditChampionDialog>("Edit Champion", dialogParameters, dialogOptions).Result;

            if (dialogResult is not null) await FetchChampions();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
    }

    private static string GetRestrictionLabel(ChampionDto context) =>
        context.Restrictions.Count == 0
            ? string.Empty
            : context.Restrictions.Count.ToString();
}