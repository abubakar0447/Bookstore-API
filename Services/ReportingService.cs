using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReportingService : IReportingService
{
    private readonly IReportingRepository _reportingRepository;
    private readonly IOrderRepository _orderRepository;

    public ReportingService(IReportingRepository reportingRepository, IOrderRepository orderRepository)
    {
        _reportingRepository = reportingRepository;
        _orderRepository = orderRepository;
    }

    // Get sales report for a given period
    public async Task<IEnumerable<SalesReportDTO>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
    {
        var orders = await _reportingRepository.GetOrdersBetweenDatesAsync(startDate, endDate);
        
        var salesReport = orders
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new SalesReportDTO
            {
                Date = g.Key,
                TotalOrders = g.Count(),
                TotalSales = g.Sum(o => o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice)),
                TotalBooksSold = g.Sum(o => o.OrderDetails.Sum(od => od.Quantity))
            })
            .ToList();

        return salesReport;
    }

    // Get books that are low in stock
    public async Task<IEnumerable<InventoryReportDTO>> GetLowStockBooksAsync(int threshold = 5)
    {
        var books = await _reportingRepository.GetAllBooksAsync();
        var lowStockBooks = books
            .Where(b => b.StockQuantity <= threshold)
            .Select(b => new InventoryReportDTO
            {
                BookId = b.Id,
                BookTitle = b.Title,
                StockQuantity = b.StockQuantity,
                IsLowStock = b.StockQuantity <= threshold
            })
            .ToList();

        return lowStockBooks;
    }

    // Get top customers based on the number of orders and spending
    public async Task<IEnumerable<UserActivityReportDTO>> GetTopCustomersAsync()
    {
        var orders = await _reportingRepository.GetAllOrdersAsync();
        
        var topCustomers = orders
            .GroupBy(o => new { o.CustomerId, o.Customer.FirstName, o.Customer.LastName, o.Customer.Email })
            .Select(g => new UserActivityReportDTO
            {
                CustomerName = $"{g.Key.FirstName} {g.Key.LastName}",
                CustomerEmail = g.Key.Email,
                TotalOrders = g.Count(),
                TotalSpent = g.Sum(o => o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice))
            })
            .OrderByDescending(c => c.TotalSpent)
            .ToList();

        return topCustomers;
    }

    // Get top authors based on the number of books sold
    public async Task<IEnumerable<TopAuthorsReportDTO>> GetTopAuthorsAsync()
    {
        // Retrieve all orders including necessary related data
        var orders = await _orderRepository.GetAllOrdersAsync();

        // Check if there are any orders
        if (!orders.Any())
        {
            Console.WriteLine("No orders found in the database.");
            return new List<TopAuthorsReportDTO>();
        }

        // Extract order details and include only those with valid books and authors
        var orderDetails = orders
            .SelectMany(o => o.OrderDetails)
            .Where(od => od.Book != null && od.Book.Author != null) // Ensure Book and Author are not null
            .ToList();

        // Check if there are any order details with valid books and authors
        if (!orderDetails.Any())
        {
            Console.WriteLine("No order details with valid books and authors found.");
            return new List<TopAuthorsReportDTO>();
        }

        // Group by author and calculate total books sold
        var topAuthors = orderDetails
            .GroupBy(od => new { od.Book.AuthorId, od.Book.Author.Name })
            .Select(g => new TopAuthorsReportDTO
            {
                AuthorName = g.Key.Name,
                TotalBooksSold = g.Sum(od => od.Quantity)
            })
            .OrderByDescending(a => a.TotalBooksSold)
            .ToList();

        if (!topAuthors.Any())
        {
            Console.WriteLine("No top authors found after grouping by author.");
        }

        return topAuthors;
    }
}
