using BookStore.Application.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.WebApi.Filters
{
    public class FailureResultFilter : Attribute, IAsyncResultFilter
    {
        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var objectResult = context.Result as ObjectResult;

            if (objectResult != null && objectResult.Value != null)
            {
                if(objectResult.Value is Result result)
                {
                    if(result.IsFailure)
                    {
                        return Task.FromResult(new BadRequestObjectResult(result));
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
