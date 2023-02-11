using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBooksByTitle
{
    public class GetBooksByTitleHandler : IRequestHandler<GetBooksByTitle, IEnumerable<BookViewModel>>
    {
        private IBookService _service;
        public GetBooksByTitleHandler(IBookService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<BookViewModel>> Handle(GetBooksByTitle request, CancellationToken cancellationToken)
        {
            return await _service.GetByTitleAsync(request.Title, cancellationToken);
        }
    }
}
