using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Repositories.Interfaces;
using BookstoreAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<BookReadDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            // Project to DTOs
            return books.Select(b => new BookReadDTO
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                ISBN = b.ISBN,
                StockQuantity = b.StockQuantity,
                AuthorName = b.Author.Name
            }).ToList();
        }

        public async Task<BookReadDTO> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null) return null;

            // Project to DTO
            return new BookReadDTO
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                ISBN = book.ISBN,
                StockQuantity = book.StockQuantity,
                AuthorName = book.Author.Name
            };
        }

        public async Task<BookReadDTO> AddBookAsync(BookCreateDTO bookCreateDTO)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(bookCreateDTO.AuthorId);
            if (author == null)
            {
                throw new System.Exception("Author not found");
            }

            var book = new Book
            {
                Title = bookCreateDTO.Title,
                Price = bookCreateDTO.Price,
                ISBN = bookCreateDTO.ISBN,
                StockQuantity = bookCreateDTO.StockQuantity,
                AuthorId = bookCreateDTO.AuthorId

            };

            await _bookRepository.AddBookAsync(book);

            // Project to DTO
            return new BookReadDTO
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                ISBN = book.ISBN,
                StockQuantity = book.StockQuantity,
                AuthorName = book.Author.Name
            };
        }

        public async Task UpdateBookAsync(int id, BookCreateDTO bookCreateDTO)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                throw new System.Exception("Book not found");
            }

            // Update the book entity with new data
            existingBook.Title = bookCreateDTO.Title;
            existingBook.Price = bookCreateDTO.Price;
            existingBook.ISBN = bookCreateDTO.ISBN;
            existingBook.StockQuantity = bookCreateDTO.StockQuantity;
            existingBook.AuthorId = bookCreateDTO.AuthorId;

            await _bookRepository.UpdateBookAsync(existingBook);
        }

        public async Task UpdateBookStockAsync(int bookId, int quantity)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.StockQuantity = quantity;  // Update the stock quantity directly
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        // Implement SearchBooksAsync method
        public async Task<IEnumerable<Book>> SearchBooksAsync(BookSearchDTO searchDto)
        {
            return await _bookRepository.SearchBooksAsync(searchDto);
        }
    }
}
