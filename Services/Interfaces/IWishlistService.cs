using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWishlistService
{
    Task AddToWishlistAsync(string customerId, WishlistCreateDTO wishlistCreateDTO);
    Task RemoveFromWishlistAsync(int wishlistId);
    Task<IEnumerable<WishlistDTO>> GetCustomerWishlistAsync(string customerId);
}
