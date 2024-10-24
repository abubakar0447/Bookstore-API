namespace BookstoreAPI.Models.DTOs
{
    public class UserActivityReportDTO
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
