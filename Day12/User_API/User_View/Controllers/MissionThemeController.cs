using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;

namespace User_View.Controllers
{
    public class MissionThemeController : Controller
    {
        private readonly HttpClient _httpClient;

        public MissionThemeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("User_API");
        }

        public async Task<IActionResult> MissionThemeDetails()
        {
            var response = await _httpClient.GetAsync("https://localhost:7189/api/MissionTheme");

            if (response.IsSuccessStatusCode)
            {
                var skillList = await response.Content.ReadFromJsonAsync<List<MissionTheme>>();
                return View(skillList);
            }

            ViewBag.Error = "Failed to fetch data.";
            return View(new List<MissionTheme>());
        }
    }
}
