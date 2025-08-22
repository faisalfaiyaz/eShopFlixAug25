using eShopFlix.Web.Models;
using System.Text.Json;

namespace eShopFlix.Web.HttpClients;

public class CatalogServiceClient
{
    readonly HttpClient _httpClient;
    public CatalogServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/catalog/getall");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<ProductModel>? products = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return products;
        }

        return Enumerable.Empty<ProductModel>();

    }
}
