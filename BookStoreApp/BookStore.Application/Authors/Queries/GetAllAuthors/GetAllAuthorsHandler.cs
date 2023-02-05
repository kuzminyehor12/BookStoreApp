using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthors, IEnumerable<AuthorViewModel>>
    {
        private readonly IAuthorService _service;
        public GetAllAuthorsHandler(IAuthorService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<AuthorViewModel>> Handle(GetAllAuthors request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
