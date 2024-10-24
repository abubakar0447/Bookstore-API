using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]  // Only Admins can access reporting endpoints
public class ReportingController : ControllerBase
{
    private readonly IReportingService _reportingService;

    public ReportingController(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    // GET: api/reporting/sales?startDate=2023-01-01&endDate=2023-12-31
    [HttpGet("sales")]
    public async Task<IActionResult> GetSalesReport(DateTime startDate, DateTime endDate)
    {
        var report = await _reportingService.GetSalesReportAsync(startDate, endDate);
        return Ok(report);
    }

    // GET: api/reporting/low-stock
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStockBooks(int threshold = 5)
    {
        var report = await _reportingService.GetLowStockBooksAsync(threshold);
        return Ok(report);
    }

    // GET: api/reporting/top-customers
    [HttpGet("top-customers")]
    public async Task<IActionResult> GetTopCustomers()
    {
        var report = await _reportingService.GetTopCustomersAsync();
        return Ok(report);
    }

    // GET: api/reporting/top-authors
    [HttpGet("top-authors")]
    public async Task<IActionResult> GetTopAuthors()
    {
        var report = await _reportingService.GetTopAuthorsAsync();
        return Ok(report);
    }
}
