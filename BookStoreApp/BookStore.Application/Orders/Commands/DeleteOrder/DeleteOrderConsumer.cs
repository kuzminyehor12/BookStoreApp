using BookStore.Application.Common.Interfaces;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderConsumer : IConsumer<DeleteOrderEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public DeleteOrderConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(DeleteOrderEvent));
        }

        public async Task Consume(ConsumeContext<DeleteOrderEvent> context)
        {
            await _syncService.RemoveAsync(context.Message.Id);
        }
    }
}
