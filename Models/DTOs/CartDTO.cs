namespace BookstoreAPI.Models.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class CartCreateDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartUpdateDTO
    {
        public int Quantity { get; set; }
    }
}
