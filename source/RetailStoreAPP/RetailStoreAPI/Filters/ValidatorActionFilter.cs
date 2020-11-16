using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace RetailStoreAPI.Filters
{
    public class ValidatorActionFilter : ActionFilterAttribute
    {   
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Model Validation
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
