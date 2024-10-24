namespace BookstoreAPI.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        
        public string CustomerId { get; set; }  // Foreign key to ApplicationUser
        public ApplicationUser Customer { get; set; }  // Navigation property

        public int BookId { get; set; }  // Foreign key to Book
        public Book Book { get; set; }  // Navigation property
    }
}
