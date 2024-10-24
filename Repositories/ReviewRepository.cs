using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddReviewAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task<Review> GetReviewByIdAsync(int reviewId)
    {
        return await _context.Reviews
            .Include(r => r.Book)
            .Include(r => r.Customer)
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }

    public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId)
    {
        return await _context.Reviews
            .Include(r => r.Book)
            .Include(r => r.Customer)
            .Where(r => r.BookId == bookId)
            .ToListAsync();
    }

    public async Task DeleteReviewAsync(Review review)
    {
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }
}
