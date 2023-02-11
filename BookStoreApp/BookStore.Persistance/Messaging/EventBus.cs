using BookStore.Application.Common.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.DataAccess.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publisher;

        public EventBus(IPublishEndpoint publisher)
        {
            _publisher = publisher;
        }

        public Task PublishAsync(DomainEvent message, CancellationToken cancellationToken = default)
        {
            return _publisher.Publish(message, cancellationToken);
        }
    }
}
