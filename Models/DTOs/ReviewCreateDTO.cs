namespace BookstoreAPI.Models.DTOs
{
    public class ReviewCreateDTO
    {
        public int BookId { get; set; }
        public int Rating { get; set; }  // Rating value (1-5)
        public string ReviewText { get; set; }
    }
}
