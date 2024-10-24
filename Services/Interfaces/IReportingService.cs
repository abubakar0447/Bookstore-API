using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReportingService
{
    Task<IEnumerable<SalesReportDTO>> GetSalesReportAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<InventoryReportDTO>> GetLowStockBooksAsync(int threshold = 5);
    Task<IEnumerable<UserActivityReportDTO>> GetTopCustomersAsync();
    Task<IEnumerable<TopAuthorsReportDTO>> GetTopAuthorsAsync();
}
