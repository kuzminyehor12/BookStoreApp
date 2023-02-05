using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
