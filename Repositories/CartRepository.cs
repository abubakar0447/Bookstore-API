using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddToCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(Cart cart)
    {
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cart>> GetCartByCustomerIdAsync(string customerId)
    {
        return await _context.Carts
            .Include(c => c.Book)
            .Where(c => c.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Cart> GetCartItemByIdAsync(int cartId)
    {
        return await _context.Carts
            .Include(c => c.Book)
            .FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public async Task<Cart> GetCartItemByCustomerAndBookIdAsync(string customerId, int bookId)
    {
        return await _context.Carts
            .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.BookId == bookId);
    }
}
