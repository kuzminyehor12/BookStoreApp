using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetAllBooksQuery
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookViewModel>>
    {
    }
}
