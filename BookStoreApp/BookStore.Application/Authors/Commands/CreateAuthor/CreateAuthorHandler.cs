using BookStore.Application.Interfaces;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthor, bool>
    {
        private readonly IAuthorService _service;
        public CreateAuthorHandler(IAuthorService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateAuthor request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Surname = request.Surname,
                Name = request.Name
            };

            return await _service.CreateAsync(author, cancellationToken);
        }
    }
}
