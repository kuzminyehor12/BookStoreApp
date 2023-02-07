using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.UpdateBookCommand
{
    internal class UpdateBookHandler : IRequestHandler<UpdateBook, bool>
    {
        private readonly IBookService _service;
        public UpdateBookHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                AmountOnStock = request.AmountOnStock,
                ISBN = request.ISBN,
                AuthorId = request.AuthorId
            };

            return await _service.UpdateAsync(book, cancellationToken);
        }
    }
}
