using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthor, Result>
    {
        private readonly IAuthorService _service;
        public CreateAuthorHandler(IAuthorService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(CreateAuthor request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Surname = request.Surname,
                Name = request.Name
            };

            return await _service.CreateAsync(author, cancellationToken);
        }
    }
}
