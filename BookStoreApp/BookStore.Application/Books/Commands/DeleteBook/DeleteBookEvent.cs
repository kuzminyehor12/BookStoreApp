using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.DeleteBook
{
    public class DeleteBookEvent : DomainEvent
    {
        public Guid Id { get; init; }
    }
}
