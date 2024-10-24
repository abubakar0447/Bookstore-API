using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Require authorization for all cart actions
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        // POST: api/cart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartCreateDTO cartCreateDTO)
        {
            var customerId = _userManager.GetUserId(User);  // Get the currently logged-in user ID
            await _cartService.AddToCartAsync(customerId, cartCreateDTO);
            return Ok(new { Message = "Book added to cart successfully" });
        }

        // DELETE: api/cart/{cartId}
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            await _cartService.RemoveFromCartAsync(cartId);
            return Ok(new { Message = "Book removed from cart successfully" });
        }

        // PUT: api/cart/{cartId}
        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCart(int cartId, [FromBody] CartUpdateDTO cartUpdateDTO)
        {
            await _cartService.UpdateCartAsync(cartId, cartUpdateDTO);
            return Ok(new { Message = "Cart updated successfully" });
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetCustomerCart()
        {
            var customerId = _userManager.GetUserId(User);
            var cartItems = await _cartService.GetCustomerCartAsync(customerId);
            return Ok(cartItems);
        }
    }
}