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
    [Authorize]  // Require authorization for all wishlist actions
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistController(IWishlistService wishlistService, UserManager<ApplicationUser> userManager)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistCreateDTO wishlistCreateDTO)
        {
            var customerId = _userManager.GetUserId(User);  // Get the currently logged-in user ID
            await _wishlistService.AddToWishlistAsync(customerId, wishlistCreateDTO);
            return Ok(new { Message = "Book added to wishlist successfully" });
        }

        [HttpDelete("{wishlistId}")]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistId)
        {
            await _wishlistService.RemoveFromWishlistAsync(wishlistId);
            return Ok(new { Message = "Book removed from wishlist successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerWishlist()
        {
            var customerId = _userManager.GetUserId(User);
            var wishlist = await _wishlistService.GetCustomerWishlistAsync(customerId);
            return Ok(wishlist);
        }
    }
}