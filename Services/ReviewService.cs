using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Repositories.Interfaces;
using BookstoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookstoreAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewService(IReviewRepository reviewRepository, IBookRepository bookRepository, UserManager<ApplicationUser> userManager)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        public async Task AddReviewAsync(string customerId, ReviewCreateDTO reviewCreateDTO)
        {
            var book = await _bookRepository.GetBookByIdAsync(reviewCreateDTO.BookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            var review = new Review
            {
                BookId = reviewCreateDTO.BookId,
                CustomerId = customerId,
                Rating = reviewCreateDTO.Rating,
                ReviewText = reviewCreateDTO.ReviewText
            };

            await _reviewRepository.AddReviewAsync(review);
        }

        public async Task UpdateReviewAsync(int reviewId, ReviewCreateDTO reviewCreateDTO)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                throw new Exception("Review not found");
            }

            review.Rating = reviewCreateDTO.Rating;
            review.ReviewText = reviewCreateDTO.ReviewText;
            review.UpdatedAt = DateTime.UtcNow;

            await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByBookIdAsync(int bookId)
        {
            var reviews = await _reviewRepository.GetReviewsByBookIdAsync(bookId);
            return reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                BookTitle = r.Book.Title,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                throw new Exception("Review not found");
            }

            await _reviewRepository.DeleteReviewAsync(review);
        }
    }
}