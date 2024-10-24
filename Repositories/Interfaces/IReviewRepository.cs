using BookstoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReviewRepository
{
    Task AddReviewAsync(Review review);
    Task UpdateReviewAsync(Review review);
    Task<Review> GetReviewByIdAsync(int reviewId);
    Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId);
    Task DeleteReviewAsync(Review review);
}
