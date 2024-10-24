namespace BookstoreAPI.Models
{
     public class Inventory
    {
        public int Id { get; set; }

        // Foreign key for Book
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int QuantityInStock { get; set; }
    }
}