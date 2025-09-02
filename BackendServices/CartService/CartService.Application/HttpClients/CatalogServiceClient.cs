using CartService.Application.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace CartService.Application.HttpClients;

public class CatalogServiceClient
{
    private readonly HttpClient _httpClient;

    public CatalogServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByIdsAsync(int[] ids)
    {
        if (ids is null || ids.Length == 0)
            return Enumerable.Empty<ProductDTO>();

        var response = await _httpClient.PostAsJsonAsync("api/Catalog/GetByIds", ids);

        if (!response.IsSuccessStatusCode)
            return Enumerable.Empty<ProductDTO>();

        var stream = await response.Content.ReadAsStreamAsync();
        var products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductDTO>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return products ?? Enumerable.Empty<ProductDTO>();
    }

}
