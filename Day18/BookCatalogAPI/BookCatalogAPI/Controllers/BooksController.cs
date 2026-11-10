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
        private readonly IValidator<CreateBookRequest> _createBookValidator;
        private readonly IValidator<UpdateTitleRequest> _updateTitleValidator;

        public BooksController(IBookService service, IValidator<CreateBookRequest> createBookValidator, IValidator<UpdateTitleRequest> updateTitleValidator)
        {
            _service = service;
            _createBookValidator = createBookValidator;
            _updateTitleValidator = updateTitleValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponse>>> GetAll([FromQuery] string? author)
        {
            var books = await _service.GetAllAsync(author);
            return Ok(books.Select(BookResponse.FromBook));
        }

        [HttpGet("{id:int}")]
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
            var result = await _createBookValidator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var book = await _service.CreateAsync(request.title, request.author, request.year);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, BookResponse.FromBook(book));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool res = await _service.DeleteAsync(id);
            if (!res)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:int}/title")]
        public async Task<ActionResult<BookResponse?>> UpdateTitle([FromRoute] int id, [FromBody] UpdateTitleRequest request)
        {
            var res = await _updateTitleValidator.ValidateAsync(request);
            if (!res.IsValid)
                return BadRequest(res.Errors);

            var book = await _service.UpdateTitleAsync(id, request.title);

            if (book is null)
                return NotFound();

            return Ok(BookResponse.FromBook(book));
        }
    }
}
