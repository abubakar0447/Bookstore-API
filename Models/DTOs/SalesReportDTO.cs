namespace BookstoreAPI.Models.DTOs
{
    public class SalesReportDTO
    {
        public DateTime Date { get; set; }  // Date of sales
        public int TotalOrders { get; set; }  // Total number of orders
        public decimal TotalSales { get; set; }  // Total sales amount
        public int TotalBooksSold { get; set; }  // Total books sold
    }
}
