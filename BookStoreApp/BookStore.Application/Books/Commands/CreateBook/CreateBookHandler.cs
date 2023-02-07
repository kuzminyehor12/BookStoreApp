using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using MediatR;

namespace BookStore.Application.Books.Commands.CreateBookCommand
{
    public class CreateBookHandler : IRequestHandler<CreateBook, bool>
    {
        private readonly IBookService _service;
        public CreateBookHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price ?? default,
                AmountOnStock = request.AmountOnStock ?? default,
                ISBN = request.ISBN,
                AuthorId = request.AuthorId,
                IsDeleted = false
            };

            return await _service.CreateAsync(book, cancellationToken);
        }
    }
}
