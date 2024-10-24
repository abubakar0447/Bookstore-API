using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICartService
{
    Task AddToCartAsync(string customerId, CartCreateDTO cartCreateDTO);
    Task RemoveFromCartAsync(int cartId);
    Task UpdateCartAsync(int cartId, CartUpdateDTO cartUpdateDTO);
    Task<IEnumerable<CartDTO>> GetCustomerCartAsync(string customerId);
}
