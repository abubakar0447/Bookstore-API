using Microsoft.AspNetCore.Mvc;
using BookstoreAPI.Services.Interfaces;
using BookstoreAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookstoreAPI.Models;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Require authorization for all review-related actions
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        // POST: api/review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            var customerId = _userManager.GetUserId(User);  // Get the currently logged-in user ID
            await _reviewService.AddReviewAsync(customerId, reviewCreateDTO);
            return Ok(new { Message = "Review added successfully" });
        }

        // PUT: api/review/{reviewId}
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            await _reviewService.UpdateReviewAsync(reviewId, reviewCreateDTO);
            return Ok(new { Message = "Review updated successfully" });
        }

        // GET: api/review/book/{bookId}
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(int bookId)
        {
            var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId);
            return Ok(reviews);
        }

        // DELETE: api/review/{reviewId}
        [HttpDelete("{reviewId}")]
        [Authorize(Roles = "Admin")]  // Only admins can delete reviews
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return Ok(new { Message = "Review deleted successfully" });
        }
    }
}