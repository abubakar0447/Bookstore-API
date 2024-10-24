using BookstoreAPI.Models.DTOs;


namespace BookstoreAPI.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(string customerId, ReviewCreateDTO reviewCreateDTO);
        Task UpdateReviewAsync(int reviewId, ReviewCreateDTO reviewCreateDTO);
        Task<IEnumerable<ReviewDTO>> GetReviewsByBookIdAsync(int bookId);
        Task DeleteReviewAsync(int reviewId);
    }
}