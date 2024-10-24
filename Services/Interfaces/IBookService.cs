using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookReadDTO>> GetAllBooksAsync();
        Task<BookReadDTO> GetBookByIdAsync(int id);
        Task<BookReadDTO> AddBookAsync(BookCreateDTO bookCreateDTO);
        Task UpdateBookAsync(int id, BookCreateDTO bookCreateDTO);
        Task UpdateBookStockAsync(int bookId, int quantity);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(BookSearchDTO searchDto);
    }
}
