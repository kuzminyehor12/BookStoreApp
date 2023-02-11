using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.DataAccess.Messaging
{
    public class EventBus : DomainEvent
    {
        public Task PublishAsync(DomainEvent message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
