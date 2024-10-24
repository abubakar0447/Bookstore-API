namespace BookstoreAPI.Models.DTOs
{
    public class OrderReadDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<OrderDetailDTO> Items { get; set; }
    }

    public class OrderDetailDTO
    {
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
