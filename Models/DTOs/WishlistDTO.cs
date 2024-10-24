namespace BookstoreAPI.Models.DTOs
{
    public class WishlistDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }
    }

    public class WishlistCreateDTO
    {
        public int BookId { get; set; }
    }
}
