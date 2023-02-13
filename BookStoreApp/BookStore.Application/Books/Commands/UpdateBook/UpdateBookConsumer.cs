using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.UpdateBook
{
    public class UpdateBookConsumer : IConsumer<UpdateBookEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public UpdateBookConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(UpdateBookEvent));
        }

        public async Task Consume(ConsumeContext<UpdateBookEvent> context)
        {
            await _syncService.ReplaceAsync(context.Message);
        }
    }
}
