using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IBookRepository _bookRepository;

    public WishlistService(IWishlistRepository wishlistRepository, IBookRepository bookRepository)
    {
        _wishlistRepository = wishlistRepository;
        _bookRepository = bookRepository;
    }

    public async Task AddToWishlistAsync(string customerId, WishlistCreateDTO wishlistCreateDTO)
    {
        var book = await _bookRepository.GetBookByIdAsync(wishlistCreateDTO.BookId);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        var wishlist = new Wishlist
        {
            CustomerId = customerId,
            BookId = wishlistCreateDTO.BookId
        };

        await _wishlistRepository.AddToWishlistAsync(wishlist);
    }

    public async Task RemoveFromWishlistAsync(int wishlistId)
    {
        var wishlist = await _wishlistRepository.GetWishlistByIdAsync(wishlistId);
        if (wishlist == null)
        {
            throw new Exception("Wishlist item not found");
        }

        await _wishlistRepository.RemoveFromWishlistAsync(wishlist);
    }

    public async Task<IEnumerable<WishlistDTO>> GetCustomerWishlistAsync(string customerId)
    {
        var wishlist = await _wishlistRepository.GetWishlistByCustomerIdAsync(customerId);
        return wishlist.Select(w => new WishlistDTO
        {
            Id = w.Id,
            BookId = w.BookId,
            BookTitle = w.Book.Title,
            BookPrice = w.Book.Price
        }).ToList();
    }
}
