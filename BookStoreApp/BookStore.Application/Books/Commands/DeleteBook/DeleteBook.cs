using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBook : IRequest<Result>
    {
        public Guid Id { get; init; }
    }
}
