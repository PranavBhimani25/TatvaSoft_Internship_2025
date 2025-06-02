using Book_View.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Book_View.Services
{
    public class BookService : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _clientFactory;

        public BookService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
        {
            _clientFactory = clientFactory;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<BookDetails>> GetAllBooksAsync()
        {
            var token = _contextAccessor.HttpContext?.Session.GetString("JWToken");
            var client = _clientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("https://localhost:7241/api/Book/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var books = JsonSerializer.Deserialize<List<BookDetails>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return books ?? new List<BookDetails>();
            }

            return new List<BookDetails>();

        }

        public async Task<BookDetails?> GetBookByIdAsync(int id)
        {
            var token = _contextAccessor.HttpContext?.Session.GetString("JWToken");
            var client = _clientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"https://localhost:7241/api/Book/GetById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var book = JsonSerializer.Deserialize<BookDetails>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return book;
            }

            return null;
        }

        public async Task<bool> AddBookAsync(BookDetails book)
        {
            var token = _contextAccessor.HttpContext?.Session.GetString("JWToken");
            var client = _clientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var json = JsonSerializer.Serialize(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7241/api/Book/Add", content);

            return response.IsSuccessStatusCode;
        }
    }
}