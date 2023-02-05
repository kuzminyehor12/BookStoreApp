using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBookByIsbn
{
    public class GetBookByIsbnHandler : IRequestHandler<GetBookByIsbn, BookViewModel>
    {
        private readonly IBookService _service;
        public GetBookByIsbnHandler(IBookService service)
        {
            _service = service;
        }

        public async Task<BookViewModel> Handle(GetBookByIsbn request, CancellationToken cancellationToken)
        {
            return await _service.GetByIsbnAsync(request.Isbn, cancellationToken);
        }
    }
}
