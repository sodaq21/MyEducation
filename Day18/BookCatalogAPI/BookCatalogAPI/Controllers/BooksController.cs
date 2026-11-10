using BookCatalogAPI.DTOs;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services;
using BookCatalogAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IValidator<CreateBookRequest> _validator;

        public BooksController(IBookService service, IValidator<CreateBookRequest> validator)
        {
            _service = service;
            _validator = validator;
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

        [HttpPost]
        public async Task<ActionResult<BookResponse>> Create(CreateBookRequest request)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var book = await _service.CreateAsync(request.title, request.author, request.year);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, BookResponse.FromBook(book));
        }
    }
}
