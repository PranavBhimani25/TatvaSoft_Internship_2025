using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User_View.Services;

namespace User_View.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid)
                return View("LoginPage", loginUser);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7189/api/Login/LoginUser", loginUser);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ResponseResult>();

                if (apiResponse?.Result == ResponseStatus.Success)
                {
                    // TODO: Save user info/token if needed
                    // Redirect to home/dashboard
                    return RedirectToAction("ViewAllUser", "User");
                }

                ModelState.AddModelError("", apiResponse?.Message ?? "Login failed.");
            }
            else
            {
                ModelState.AddModelError("", "Unable to reach login service.");
            }

            return View("LoginPage", loginUser);
        }
    }

}
