using BookCatalogAPI.DTOs;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponse>>> GetAll([FromQuery] string? author)
        {
            var books = await _service.GetAllAsync(author);
            return Ok(books.Select(BookResponse.FromBook));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponse?>> GetById([FromRoute] int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book is null)
                return NotFound();

            return Ok(BookResponse.FromBook(book));
        }

        //[HttpPost]
        //public async Task<ActionResult<BookResponse>> Create(CreateBookRequest request)
        //{

        //}
    }
}
