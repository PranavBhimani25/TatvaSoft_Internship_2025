using Book_View.Models;
using Book_View.Models.ViewModel;
using System.Text;
using System.Text.Json;

namespace Book_View.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<string?> LoginAsync(LoginModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7241/api/User/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(res, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return loginResponse?.Token;
            }

            return null;
        }
    }
}
