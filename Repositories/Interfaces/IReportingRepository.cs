using BookstoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReportingRepository
{
    // Method to retrieve orders between specific dates for sales reporting
    Task<IEnumerable<Order>> GetOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate);

    // Method to retrieve all orders for customer activity or other reports
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    // Method to get all books with their stock quantities
    Task<IEnumerable<Book>> GetAllBooksAsync();

    // Method to get all authors and their respective book sales count
    // Task<IEnumerable<Author>> GetTopAuthorsByBookSalesAsync();
}
