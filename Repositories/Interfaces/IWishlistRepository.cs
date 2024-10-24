using BookstoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWishlistRepository
{
    Task AddToWishlistAsync(Wishlist wishlist);
    Task RemoveFromWishlistAsync(Wishlist wishlist);
    Task<IEnumerable<Wishlist>> GetWishlistByCustomerIdAsync(string customerId);
    Task<Wishlist> GetWishlistByIdAsync(int wishlistId);
}
