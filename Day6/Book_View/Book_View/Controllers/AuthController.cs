using Book_View.Models.ViewModel;
using Book_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_View.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _authService.LoginAsync(model);

            if (token != null)
            {
                // Save JWT to cookie or session
                HttpContext.Session.SetString("JWToken", token);
                
                return RedirectToAction("Home", "Admin"); // or Books/Index
            }

            ViewBag.Error = "Invalid email or password.";
            return View(model);

        }
    }
}
