using Microsoft.AspNetCore.Identity;

namespace BookstoreAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        // You can extend this class with additional properties if required.
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
