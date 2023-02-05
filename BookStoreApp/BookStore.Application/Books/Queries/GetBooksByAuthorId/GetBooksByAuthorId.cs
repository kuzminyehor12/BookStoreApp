using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBooksByAuthorId
{
    public class GetBooksByAuthorId : IRequest<IEnumerable<BookViewModel>>
    {
        public Guid AuthorId { get; set; }
    }
}
