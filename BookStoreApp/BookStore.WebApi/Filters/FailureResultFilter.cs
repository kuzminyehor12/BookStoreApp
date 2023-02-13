using BookStore.Application.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.WebApi.Filters
{
    public class FailureResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = context.Result as ObjectResult;

            if (objectResult != null && objectResult.Value != null)
            {
                if (objectResult.Value is Result result)
                {
                    if (result.IsFailure)
                    {
                        context.Result = new BadRequestObjectResult(result);
                    }
                }
            }
        }
    }
}
