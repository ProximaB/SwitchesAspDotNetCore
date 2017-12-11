using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SwitchesAPI.Common;

namespace SwitchesAPI.Filters
{
    public class SwitchApiExceptionFilterAttribute : ExceptionFilterAttribute
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
