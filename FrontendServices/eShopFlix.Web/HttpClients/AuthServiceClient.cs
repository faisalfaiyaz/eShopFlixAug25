using eShopFlix.Web.Models;
using System.Text;
using System.Text.Json;

namespace eShopFlix.Web.HttpClients;

public class AuthServiceClient
{
    private readonly HttpClient _httpClient;

    public AuthServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<UserModel> LoginAsync(LoginModel loginModel)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(loginModel),
            Encoding.UTF8,
            "application/json" 
        ); 
        
        HttpResponseMessage response = await _httpClient.PostAsync("auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            if (responseContent != null)
            {
                UserModel user = JsonSerializer.Deserialize<UserModel>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;

                return user;
            }
        }

        return null;
    }

    public async Task<bool> RegisterAsync(SignUpModel signUpModel)
    {
        StringContent content = new StringContent(
                      JsonSerializer.Serialize(signUpModel), 
                      Encoding.UTF8, 
                      "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("auth/signup", content);

        return response.IsSuccessStatusCode ? true : false;
    }
}
