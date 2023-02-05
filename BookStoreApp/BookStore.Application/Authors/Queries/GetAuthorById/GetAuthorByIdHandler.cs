using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorById, AuthorViewModel>
    {
        private readonly IAuthorService _service;
        public GetAuthorByIdHandler(IAuthorService service)
        {
            _service = service;
        }

        public async Task<AuthorViewModel> Handle(GetAuthorById request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
