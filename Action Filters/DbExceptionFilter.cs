using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace BookstoreAPI.Filters
{
    public class DbExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<DbExceptionFilter> _logger;

        public DbExceptionFilter(ILogger<DbExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Handle database-related exceptions
            if (context.Exception is DbUpdateException)
            {
                _logger.LogError(context.Exception, "A database update error occurred.");
                context.Result = new ObjectResult(new { message = "A database error occurred while processing your request." })
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is InvalidOperationException && context.Exception.Message.Contains("Sequence"))
            {
                _logger.LogError(context.Exception, "An invalid operation exception occurred.");
                context.Result = new ObjectResult(new { message = "Invalid operation detected during database query." })
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }
            else
            {
                // Log other unhandled exceptions (you can customize this part)
                _logger.LogError(context.Exception, "An unexpected error occurred.");
                context.Result = new ObjectResult(new { message = "An unexpected error occurred." })
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
