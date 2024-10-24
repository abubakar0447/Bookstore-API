using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;

namespace BookstoreAPI.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request
            Log.Information($"Incoming Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the outgoing response
            Log.Information($"Outgoing Response: {context.Response.StatusCode}");
        }
    }
}
