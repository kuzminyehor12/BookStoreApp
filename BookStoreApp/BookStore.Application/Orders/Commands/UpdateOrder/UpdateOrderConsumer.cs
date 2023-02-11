using BookStore.Application.Common.Interfaces;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderConsumer : IConsumer<UpdateOrderEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public UpdateOrderConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(UpdateOrderEvent));
        }

        public async Task Consume(ConsumeContext<UpdateOrderEvent> context)
        {
            await _syncService.ReplaceAsync(context.Message);
        }
    }
}
