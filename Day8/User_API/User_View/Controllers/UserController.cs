using Microsoft.AspNetCore.Mvc;
using User_View.Services;

namespace User_View.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> ViewAllUser()
        {
            var books = await _userService.GetAllUsersAsync();
            return View(books);
        }

        [HttpPost]
        public IActionResult RedirectToUser(int bookId)
        {
            return RedirectToAction("UserById", new { id = bookId });
        }

        public async Task<IActionResult> UserById(int id)
        {
            var book = await _userService.GetUserByIdAsync(id);

            if (book == null)
            {
                TempData["Error"] = $"Book with ID {id} not found.";
                return RedirectToAction("GetBook");
            }

            return View(book);
        }

        public IActionResult UserDashboard()
        {
            return View();
        }
    }
}
