using BookStore.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Messaging
{
    public abstract class DomainEvent
    {
        public string Result { get; set; }
        public Guid Id { get; init; }
    }

    public class BookEvent : DomainEvent { }
    public class AuthorEvent : DomainEvent { }
    public class DetailEvent : DomainEvent { }
    public class OrderEvent : DomainEvent { }
}
