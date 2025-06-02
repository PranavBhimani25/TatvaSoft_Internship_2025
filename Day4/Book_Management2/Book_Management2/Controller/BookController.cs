using Book.DataAccess.Models;
using Book.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management2.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _service;

        public BooksController(BooksService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            return book is null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Mybooks book) =>
            Ok(await _service.AddAsync(book));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Mybooks book)
        {
            var updated = await _service.UpdateAsync(id, book);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByGenre([FromQuery] string genre) =>
            Ok(await _service.FilterByGenreAsync(genre));
    }
}
