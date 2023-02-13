using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBooksByAuthorId
{
    public class GetBooksByAuthorIdHandler : IRequestHandler<GetBooksByAuthorId, IEnumerable<BookViewModel>>
    {
        private readonly IBookService _service;
        public GetBooksByAuthorIdHandler(IBookService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<BookViewModel>> Handle(GetBooksByAuthorId request, CancellationToken cancellationToken)
        {
            return await _service.GetByAuthorIdAsync(request.AuthorId, cancellationToken);
        }
    }
}
