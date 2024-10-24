using Microsoft.AspNetCore.Mvc;
using BookstoreAPI.Services.Interfaces;
using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookstoreAPI.Filters;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadDTO>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookReadDTO>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Books
        [ServiceFilter(typeof(DbExceptionFilter))]
        [Authorize]
        [HttpPost("addBook")]
        // [Route("AddBook")]
        public async Task<ActionResult<BookReadDTO>> AddBook([FromBody] BookCreateDTO bookCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await _bookService.AddBookAsync(bookCreateDTO);

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        // PUT: api/Books/{id}
        [ServiceFilter(typeof(DbExceptionFilter))]
        [Authorize]
        [HttpPut("update/{id}")]
        // [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookCreateDTO bookCreateDTO)
        {
            await _bookService.UpdateBookAsync(id, bookCreateDTO);
            return NoContent();
        }

        // DELETE: api/Books/{id}
        [Authorize]
        [HttpDelete("delete/{id}")]
        // [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        // POST: api/book/search
        [HttpPost("search")]
        public async Task<IActionResult> SearchBooks([FromBody] BookSearchDTO searchDto)
        {
            var books = await _bookService.SearchBooksAsync(searchDto);
            return Ok(books);
        }
    }
}
