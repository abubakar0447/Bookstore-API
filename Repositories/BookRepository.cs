// using BookstoreAPI.Modules;
using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
// using BookstoreAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookstoreAPI.Repositories.Interfaces;


namespace BookstoreAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }
            return book;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(BookSearchDTO searchDto)
        {
            // Start with the base query
            var query = _context.Books
                .Include(b => b.Author)  // Include Author
                // .Include(b => b.OrderDetails)  // Include OrderDetails
                .Include(b => b.Reviews)  // Include Reviews
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
            {
                query = query.Where(b => b.Title.Contains(searchDto.Title));
            }

            if (!string.IsNullOrEmpty(searchDto.Author))
            {
                query = query.Where(b => b.Author.Name.Contains(searchDto.Author));
            }

            if (!string.IsNullOrEmpty(searchDto.ISBN))
            {
                query = query.Where(b => b.ISBN == searchDto.ISBN);
            }

            if (searchDto.MinPrice.HasValue)
            {
                query = query.Where(b => b.Price >= searchDto.MinPrice.Value);
            }

            if (searchDto.MaxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= searchDto.MaxPrice.Value);
            }

            return await query.ToListAsync();
        }
    }
}
