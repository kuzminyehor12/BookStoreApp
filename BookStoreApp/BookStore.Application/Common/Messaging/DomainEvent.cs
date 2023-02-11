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
        public Result Result { get; init; }
    }
}
