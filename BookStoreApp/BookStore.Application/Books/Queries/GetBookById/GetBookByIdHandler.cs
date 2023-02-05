using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBookByIdQuery
{
    public class GetBookByIdHandler : IRequestHandler<GetBookById, BookViewModel>
    {
        private IBookService _service;
        public GetBookByIdHandler(IBookService service)
        {
            _service = service;
        }

        public async Task<BookViewModel> Handle(GetBookById request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
