using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]  // Only admins can manage inventory
    public class InventoryController : ControllerBase
    {
        private readonly IBookService _bookService;

        public InventoryController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/inventory/books
        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/inventory/book/{bookId}
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }
            return Ok(book);
        }

        // PUT: api/inventory/book/{bookId}/addStock
        [HttpPut("book/{bookId}/addStock")]
        public async Task<IActionResult> AddStock(int bookId, [FromBody] int quantity)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            // Increment stock quantity by the given quantity
            book.StockQuantity += quantity;

            // Use the new UpdateBookStockAsync method
            await _bookService.UpdateBookStockAsync(bookId, book.StockQuantity);

            return Ok(new { Message = $"Stock updated. New stock quantity: {book.StockQuantity}" });
        }

        // PUT: api/inventory/book/{bookId}/removeStock
        [HttpPut("book/{bookId}/removeStock")]
        public async Task<IActionResult> RemoveStock(int bookId, [FromBody] int quantity)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            if (book.StockQuantity < quantity)
            {
                return BadRequest(new { Message = "Not enough stock to remove" });
            }

            // Remove stock from the book
            book.StockQuantity -= quantity;
            
            // Use the new UpdateBookStockAsync method
            await _bookService.UpdateBookStockAsync(bookId, book.StockQuantity);

            return Ok(new { Message = $"Stock updated. New stock quantity: {book.StockQuantity}" });
        }
    }
}
