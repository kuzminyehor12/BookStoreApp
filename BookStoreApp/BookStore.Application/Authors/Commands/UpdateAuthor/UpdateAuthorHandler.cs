using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, bool>
    {
        private readonly IAuthorService _service;
        public UpdateAuthorHandler(IAuthorService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Id = request.Id,
                Surname = request.Surname,
                Name = request.Name,
            };

            return await _service.UpdateAsync(author, cancellationToken);
        }
    }
}
