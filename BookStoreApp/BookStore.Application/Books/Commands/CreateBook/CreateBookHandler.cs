using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using MediatR;

namespace BookStore.Application.Books.Commands.CreateBook
{ 
    public class CreateBookHandler : IRequestHandler<CreateBook, Result>
    {
        private readonly IBookService _service;
        public CreateBookHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<Result> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
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
