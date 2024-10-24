using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReportingRepository : IReportingRepository
{
    private readonly ApplicationDbContext _context;

    public ReportingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Retrieve orders between specific dates for sales reporting
    public async Task<IEnumerable<Order>> GetOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Orders
            .Include(o => o.Customer)  // Include customer details
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)  // Include book details in order items
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .ToListAsync();
    }

    // Retrieve all orders for customer activity or other reports
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)
            .AsNoTracking()
            .ToListAsync();
    }

    // Retrieve all books along with their stock quantities
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .ToListAsync();
    }

    // Get all authors and their respective book sales count
    // public async Task<IEnumerable<Author>> GetTopAuthorsByBookSalesAsync()
    // {
    //     var topAuthors = await _context.OrderDetails
    //         .Include(od => od.Book)
    //             .ThenInclude(b => b.Author)  // Include author details
    //         .GroupBy(od => od.Book.Author)
    //         .Select(g => new Author
    //         {
    //             Id = g.Key.Id,
    //             Name = g.Key.Name,
    //             Books = g.Key.Books,
    //             // Count the total books sold for each author
    //             BooksSold = g.Sum(od => od.Quantity)
    //         })
    //         .OrderByDescending(a => a.BooksSold)
    //         .ToListAsync();

    //     return topAuthors;
    // }
}
