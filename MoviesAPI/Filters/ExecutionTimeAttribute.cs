using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesAPI.Filters
{
    public class ExecutionTimeAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            context.HttpContext.Response.Headers.Add("Elapsed-Time", stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}
