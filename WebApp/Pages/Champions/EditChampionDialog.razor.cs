using System.Configuration;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using SharedKernel.Contracts.v1;
using SharedKernel.Contracts.v1.Champions;

namespace WebApp.Pages.Champions;

public partial class EditChampionDialog
{
    //TODO add restriction target icon art as background 
    //TODO select restriction target from list of all available maybe ?
    //TODO restriction combos

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    [Parameter] public string Id { get; set; }
    [Parameter] public string Name { get; set; }
    [Parameter] public string Role { get; set; }
    [Parameter] public List<ChampionRestrictionDto> Restrictions { get; set; } = new();

    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private HttpClient HttpClient { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private bool isBusy;

    public IEnumerable<MudColor> RestrictionColorPalette { get; set; }

    protected override void OnInitialized()
    {
        string[]? palette = Configuration.GetSection("ChampionRestrictions:ColorPalette").Get<string[]>()
            ?? throw new ConfigurationErrorsException("ChampionRestrictions:ColorPalette not found in configuration.");
        RestrictionColorPalette = palette.Select(color => new MudColor(color)).ToArray();
    }

    private Task AddAugment() => throw new NotImplementedException();
    private Task UploadBackground() => throw new NotImplementedException();

    private async Task AddRestriction()
    {
        if (isBusy) return;
        isBusy = true;

        AddRestrictionRequest request = new(Id, "Ability", "Q", "#FF0000", "Reason");
        try
        {
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("champions/restrict", request);

            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Restriction added successfully!", Severity.Success);
                await FetchChampion();
            }
            else
            {
                await HandleErrorResponse(response);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
        finally
        {
            isBusy = false;
        }
    }

    private async Task RemoveRestriction(ChampionRestrictionDto restriction)
    {
        if (isBusy) return;
        isBusy = true;

        RemoveRestrictionRequest request = new(Id, restriction.RestrictionId);
        try
        {
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("champions/un-restrict", request);

            if (response.IsSuccessStatusCode)
            {
                Restrictions.Remove(restriction);
                Snackbar.Add("Restriction removed successfully!", Severity.Success);
            }
            else
            {
                await HandleErrorResponse(response);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
        finally
        {
            isBusy = false;
        }
    }

    private async Task UpdateRestriction(ChampionRestrictionDto restriction)
    {
        if (isBusy) return;
        isBusy = true;

        UpdateRestrictionRequest request = new(restriction.RestrictionId, restriction.AbilityName, restriction.Identifier, restriction.Color, restriction.Reason);

        try
        {
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("champions/update-restriction", request);

            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Restriction updated successfully!", Severity.Success);
                await FetchChampion();
            }
            else
            {
                await HandleErrorResponse(response);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
        finally
        {
            isBusy = false;
        }
    }

    private async Task FetchChampion()
    {
        try
        {
            ChampionDto? champion = await HttpClient.GetFromJsonAsync<ChampionDto>($"champions/{Id}");

            if (champion is null)
            {
                Snackbar.Add("Champion not found.", Severity.Error);
                return;
            }

            Id = champion.Id;
            Name = champion.Name;
            Role = champion.Role;
            Restrictions = champion.Restrictions;

            StateHasChanged();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
    }

    private async Task HandleErrorResponse(HttpResponseMessage response)
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
