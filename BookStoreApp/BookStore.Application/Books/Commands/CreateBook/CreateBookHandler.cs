using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using MediatR;

namespace BookStore.Application.Books.Commands.CreateBookCommand
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IBookService _service;
        public CreateBookHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                AmountOnStock = request.AmountOnStock,
                ISBN = request.ISBN,
                AuthorId = request.AuthorId,
                IsDeleted = false
            };

            return await _service.CreateAsync(book, cancellationToken);
        }
    }
}
