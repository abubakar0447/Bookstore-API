using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Repositories.Interfaces;
using BookstoreAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IBookRepository _bookRepository;

    public CartService(ICartRepository cartRepository, IBookRepository bookRepository)
    {
        _cartRepository = cartRepository;
        _bookRepository = bookRepository;
    }

    // Add book to cart
    public async Task AddToCartAsync(string customerId, CartCreateDTO cartCreateDTO)
    {
        var book = await _bookRepository.GetBookByIdAsync(cartCreateDTO.BookId);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        // Check if book is already in the cart, if so, update the quantity
        var existingCartItem = await _cartRepository.GetCartItemByCustomerAndBookIdAsync(customerId, cartCreateDTO.BookId);
        if (existingCartItem != null)
        {
            existingCartItem.Quantity += cartCreateDTO.Quantity;
            await _cartRepository.UpdateCartAsync(existingCartItem);
        }
        else
        {
            var cartItem = new Cart
            {
                CustomerId = customerId,
                BookId = cartCreateDTO.BookId,
                Quantity = cartCreateDTO.Quantity
            };

            await _cartRepository.AddToCartAsync(cartItem);
        }
    }

    // Remove book from cart
    public async Task RemoveFromCartAsync(int cartId)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(cartId);
        if (cartItem == null)
        {
            throw new Exception("Cart item not found");
        }

        await _cartRepository.RemoveFromCartAsync(cartItem);
    }

    // Update book quantity in cart
    public async Task UpdateCartAsync(int cartId, CartUpdateDTO cartUpdateDTO)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(cartId);
        if (cartItem == null)
        {
            throw new Exception("Cart item not found");
        }

        cartItem.Quantity = cartUpdateDTO.Quantity;
        await _cartRepository.UpdateCartAsync(cartItem);
    }

    // Get all cart items for a customer
    public async Task<IEnumerable<CartDTO>> GetCustomerCartAsync(string customerId)
    {
        var cartItems = await _cartRepository.GetCartByCustomerIdAsync(customerId);
        return cartItems.Select(ci => new CartDTO
        {
            Id = ci.Id,
            BookId = ci.BookId,
            BookTitle = ci.Book.Title,
            BookPrice = ci.Book.Price,
            Quantity = ci.Quantity
        }).ToList();
    }
}
