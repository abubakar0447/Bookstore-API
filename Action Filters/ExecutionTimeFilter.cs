using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BookstoreAPI.Filters
{
    public class ExecutionTimeFilter : IActionFilter
    {
        private readonly ILogger<ExecutionTimeFilter> _logger;
        private Stopwatch _stopwatch;

        public ExecutionTimeFilter(ILogger<ExecutionTimeFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} executed in {elapsedMilliseconds}ms");
        }
    }
}
