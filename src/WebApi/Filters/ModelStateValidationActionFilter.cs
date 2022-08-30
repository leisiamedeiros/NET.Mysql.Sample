using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET.Mysql.Sample.WebApi.Transport;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.WebApi.Filters
{
    public class ModelStateValidationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResponse = ErrorResponse.GetErrorResponseFromModelState(context.ModelState);

                context.Result = new BadRequestObjectResult(errorResponse);
            }
            else
            {
                await next();
            }
        }
    }
}
