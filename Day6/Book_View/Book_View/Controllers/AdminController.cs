using Book_View.Models;
using Book_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_View.Controllers
{
    public class AdminController : Controller
    {
        private readonly BookService _bookService;

        public AdminController (BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> GetBook()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpPost]
        public IActionResult RedirectToBook(int bookId)
        {
            return RedirectToAction("Details", new { id = bookId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                TempData["Error"] = $"Book with ID {id} not found.";
                return RedirectToAction("GetBook");
            }

            return View(book);
        }

        public IActionResult AddBook()
        { 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(BookDetails book)
        {
            if (!ModelState.IsValid)
                return View(book);

            var success = await _bookService.AddBookAsync(book);

            if (success)
            {
                TempData["Success"] = "Book added successfully.";
                return RedirectToAction("GetBook");
            }

            ViewBag.Error = "Failed to add book. Please try again.";
            return View(book);

        }
    }
}
