using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetAllBooksQuery
{
    internal class GetAllBooksHandler : IRequestHandler<GetAllBooks, IEnumerable<BookViewModel>>
    {
        private readonly IBookService _service;
        public GetAllBooksHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<BookViewModel>> Handle(GetAllBooks request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
