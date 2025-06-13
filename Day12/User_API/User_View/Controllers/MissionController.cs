using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;

namespace User_View.Controllers
{
    public class MissionController : Controller
    {
        private readonly HttpClient _httpClient;

        public MissionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("User_API");
        }

        public async Task<IActionResult> MissionSkillDetail()
        {
            var response = await _httpClient.GetAsync("https://localhost:7189/api/MissionSkill");

            if (response.IsSuccessStatusCode)
            {
                var skillList = await response.Content.ReadFromJsonAsync<List<MissionSkill>>();
                return View(skillList);
            }

            ViewBag.Error = "Failed to fetch data.";
            return View(new List<MissionSkill>());
        }
    }

}

