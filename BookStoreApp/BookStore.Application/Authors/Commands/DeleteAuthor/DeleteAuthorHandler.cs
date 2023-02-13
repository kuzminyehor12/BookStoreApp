using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, Result>
    {
        private readonly IAuthorService _service;
        public DeleteAuthorHandler(IAuthorService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(DeleteAuthor request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
