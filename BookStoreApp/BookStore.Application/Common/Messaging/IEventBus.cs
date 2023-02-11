using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync(DomainEvent message, CancellationToken cancellationToken = default);
    }
}
