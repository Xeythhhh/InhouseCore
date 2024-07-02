using System.Net.Http.Json;

using SharedKernel.Contracts.Requests.Champions;
using SharedKernel.Contracts.Responses.Champions;

namespace BlazorClient;

//TODO Fix this garbage
public class ChampionService(HttpClient httpClient)
{
#pragma warning disable CS8603 // Possible null reference return. temp xd
    public async Task<GetAllChampionsResponse> GetChampionsAsync() =>
        await httpClient.GetFromJsonAsync<GetAllChampionsResponse>("api/champions");
#pragma warning restore CS8603 // Possible null reference return.

    public async Task<GetChampionResponse?> GetChampionByIdAsync(long id) =>
        await httpClient.GetFromJsonAsync<GetChampionResponse>($"api/champions/{id}");

    public async Task<long> CreateChampionAsync(CreateChampionRequest dto)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/champions", dto);

        Console.WriteLine(response.Content);

        return 123;
    }

    public async Task UpdateChampionAsync(UpdateChampionRequest dto) =>
        await httpClient.PutAsJsonAsync($"api/champions/{dto.Id}", dto);

    public async Task DeleteChampionAsync(long id) =>
        await httpClient.DeleteAsync($"api/champions/{id}");
}
