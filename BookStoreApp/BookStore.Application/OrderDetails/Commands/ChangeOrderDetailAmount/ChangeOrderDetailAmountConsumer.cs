using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount
{
    public class ChangeOrderDetailAmountConsumer : IConsumer<ChangeOrderDetailAmountEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public ChangeOrderDetailAmountConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(ChangeOrderDetailAmountEvent));
        }

        public async Task Consume(ConsumeContext<ChangeOrderDetailAmountEvent> context)
        {
            await _syncService.ReplaceAsync(context.Message);
        }
    }
}
