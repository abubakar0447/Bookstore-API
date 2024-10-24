using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _context;

    public WishlistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Add item to wishlist
    public async Task AddToWishlistAsync(Wishlist wishlist)
    {
        await _context.Wishlists.AddAsync(wishlist);
        await _context.SaveChangesAsync();
    }

    // Remove item from wishlist
    public async Task RemoveFromWishlistAsync(Wishlist wishlist)
    {
        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync();
    }

    // Retrieve wishlist by customer ID
    public async Task<IEnumerable<Wishlist>> GetWishlistByCustomerIdAsync(string customerId)
    {
        return await _context.Wishlists
            .Include(w => w.Book)
            .Where(w => w.CustomerId == customerId)
            .ToListAsync();
    }

    // Retrieve a wishlist item by ID
    public async Task<Wishlist> GetWishlistByIdAsync(int wishlistId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Book)
            .FirstOrDefaultAsync(w => w.Id == wishlistId);

        if (wishlist == null)
        {
            throw new KeyNotFoundException($"Wishlist item with ID {wishlistId} not found.");
        }

        return wishlist;
    }
}
