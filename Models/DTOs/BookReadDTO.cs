namespace BookstoreAPI.Models.DTOs
{
    public class BookReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public int StockQuantity { get; set; }
        public string AuthorName { get; set; }
    }
}
