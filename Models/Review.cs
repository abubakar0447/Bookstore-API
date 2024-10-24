namespace BookstoreAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }  // Foreign key to Book
        public Book Book { get; set; }  // Navigation property

        public string CustomerId { get; set; }  // Foreign key to ApplicationUser
        public ApplicationUser Customer { get; set; }  // Navigation property

        public int Rating { get; set; }  // Rating (1-5 stars)
        public string ReviewText { get; set; }  // Review text

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
