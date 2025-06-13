using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using User.Entities.Entities;
using System.Text.Json;

namespace User_View.Services
{
    public class UserService : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
        {
            _clientFactory = clientFactory;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<UserDetails>> GetAllUsersAsync()
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7189/api/User");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var books = JsonSerializer.Deserialize<List<UserDetails>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return books ?? new List<UserDetails>();
            }

            return new List<UserDetails>();
        }


        public async Task<UserDetails?> GetUserByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            
            var response = await client.GetAsync($"https://localhost:7189/api/User?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var book = JsonSerializer.Deserialize<UserDetails>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return book;
            }

            return null;
        }
    }
}
