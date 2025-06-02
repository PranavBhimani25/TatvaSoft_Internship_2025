using Book_Management.Model;
using Book_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookServices _bookServices;

        public BookController(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet("GetAllBook")]
        public IActionResult GetAll() => Ok(_bookServices.GetAll());

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var book = _bookServices.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost] 
        public IActionResult Create(Book book)
        {
            var newbook = _bookServices.Add(book);
            return Ok("New Book is Created !!");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            var updated = _bookServices.Update(id, book);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bookServices.Delete(id);
            return deleted ? NoContent() : NotFound();
        }



    }
}
