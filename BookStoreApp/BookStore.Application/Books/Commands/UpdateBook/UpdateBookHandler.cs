using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.UpdateBook
{
    internal class UpdateBookHandler : IRequestHandler<UpdateBook, Result>
    {
        private readonly IBookService _service;
        public UpdateBookHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<Result> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price ?? default,
                AmountOnStock = request.AmountOnStock ?? default,
                ISBN = request.ISBN,
                AuthorId = request.AuthorId
            };

            return await _service.UpdateAsync(book, cancellationToken);
        }
    }
}
