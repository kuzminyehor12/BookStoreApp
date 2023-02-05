using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetAllBooksQuery
{
    internal class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookViewModel>>
    {
        private readonly IBookService _service;
        public GetAllBooksHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<BookViewModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
