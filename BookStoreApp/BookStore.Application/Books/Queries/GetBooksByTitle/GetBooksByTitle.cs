using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBooksByTitle
{
    public class GetBooksByTitle : IRequest<IEnumerable<BookViewModel>>
    {
        public string Title { get; init; }
    }
}
