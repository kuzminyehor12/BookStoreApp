using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBookByIdQuery
{
    public class GetBookById : IRequest<BookViewModel>
    {
        public Guid Id { get; init; }
    }
}
