using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.UpdateBook
{
    public class UpdateBookEvent : BookEvent
    {
        public string ISBN { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int? AmountOnStock { get; init; }
        public decimal? Price { get; init; }
        public Guid? AuthorId { get; init; }
    }
}
