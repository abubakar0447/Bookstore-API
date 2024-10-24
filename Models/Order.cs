namespace BookstoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;  // Default status is "Pending"
        
        // Optional fields for tracking cancellations and returns
        public string? CancellationReason { get; set; }
        public string? ReturnReason { get; set; }

        // Foreign key for Customer
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        // Navigation properties
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
