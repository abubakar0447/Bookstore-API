namespace BookstoreAPI.Models.DTOs
{
    public class InventoryReportDTO
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int StockQuantity { get; set; }
        public bool IsLowStock { get; set; }  // Indicator for low stock
    }
}
