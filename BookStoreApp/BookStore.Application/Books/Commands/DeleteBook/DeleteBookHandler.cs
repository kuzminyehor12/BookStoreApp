using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookHandler : IRequestHandler<DeleteBook, bool>
    {
        private readonly IBookService _service;
        public DeleteBookHandler(IBookService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(DeleteBook request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
