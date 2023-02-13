using BookStore.Application.Common.Interfaces;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderConsumer : IConsumer<CreateOrderEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public CreateOrderConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(CreateOrderEvent));
        }

        public async Task Consume(ConsumeContext<CreateOrderEvent> context)
        {
            await _syncService.AddAsync(context.Message);
        }
    }
}
