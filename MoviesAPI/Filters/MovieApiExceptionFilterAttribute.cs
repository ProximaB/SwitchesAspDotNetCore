using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Common;

namespace MoviesAPI.Filters
{
    public class MovieApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is MovieApiException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
        }
    }
}
