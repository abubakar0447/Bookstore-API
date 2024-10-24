namespace BookstoreAPI.Models{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public int StockQuantity { get; set; }  // For inventory tracking

        // Foreign key for Author
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Navigation properties
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Inventory Inventory { get; set; } // Added navigation property
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}