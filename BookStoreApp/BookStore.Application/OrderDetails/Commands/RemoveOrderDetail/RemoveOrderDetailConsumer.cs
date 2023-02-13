using BookStore.Application.Common.Interfaces;
using BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.RemoveOrderDetail
{
    public class RemoveOrderDetailConsumer : IConsumer<RemoveOrderDetailEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public RemoveOrderDetailConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(RemoveOrderDetailEvent));
        }

        public async Task Consume(ConsumeContext<RemoveOrderDetailEvent> context)
        {
            await _syncService.RemoveAsync(context.Message.Id);
        }
    }
}
