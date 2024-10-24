using BookstoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICartRepository
{
    Task AddToCartAsync(Cart cart);
    Task RemoveFromCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task<IEnumerable<Cart>> GetCartByCustomerIdAsync(string customerId);
    Task<Cart> GetCartItemByIdAsync(int cartId);  // Get a cart item by its ID
    Task<Cart> GetCartItemByCustomerAndBookIdAsync(string customerId, int bookId);  // Get a cart item by customer and book ID
}
