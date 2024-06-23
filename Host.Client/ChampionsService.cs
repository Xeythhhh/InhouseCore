using System.Net.Http.Json;

using SharedKernel.Champions;

namespace Host.Client;

public class ChampionService(HttpClient httpClient)
{
    public async Task<List<GetChampionDto>?> GetChampionsAsync() =>
        await httpClient.GetFromJsonAsync<List<GetChampionDto>>("api/champions");

    public async Task<GetChampionDto?> GetChampionByIdAsync(long id) =>
        await httpClient.GetFromJsonAsync<GetChampionDto>($"api/champions/{id}");

    public async Task<long> CreateChampionAsync(CreateChampionDto dto)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/champions", dto);

        Console.WriteLine(response.Content);

        return 123;
    }

    public async Task UpdateChampionAsync(UpdateChampionDto dto) =>
        await httpClient.PutAsJsonAsync($"api/champions/{dto.Id}", dto);

    public async Task DeleteChampionAsync(long id) =>
        await httpClient.DeleteAsync($"api/champions/{id}");
}
