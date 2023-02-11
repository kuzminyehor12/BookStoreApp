using BookStore.Persistance.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.WebApi.Filters
{
    public class BookStoreExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BookStoreException bookStoreException)
            {
                context.Result = new BadRequestObjectResult(new { Source = bookStoreException.Source, Message = bookStoreException.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
