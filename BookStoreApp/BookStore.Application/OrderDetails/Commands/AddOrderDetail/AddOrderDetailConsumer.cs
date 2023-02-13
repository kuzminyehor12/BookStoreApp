using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.AddOrderDetail
{
    public class AddOrderDetailConsumer : IConsumer<AddOrderDetailEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public AddOrderDetailConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(AddOrderDetailEvent));
        }

        public async Task Consume(ConsumeContext<AddOrderDetailEvent> context)
        {
            await _syncService.AddAsync(context.Message);
        }
    }
}
