using BookStore.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Messaging
{
    public abstract class DomainEvent
    {
        public Guid Id { get; init; }
        public Result Result { get; init; } = Result.Success();
    }

    public class BookEvent : DomainEvent { }
    public class AuthorEvent : DomainEvent { }
    public class DetailEvent : DomainEvent { }
    public class OrderEvent : DomainEvent { }
}
