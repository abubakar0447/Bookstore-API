namespace BookstoreAPI.Models.DTOs
{
    public class OrderCreateDTO
    {
        public List<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
