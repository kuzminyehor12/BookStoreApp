using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.DeleteBook
{
    public class DeleteBookConsumer : IConsumer<DeleteBookEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public DeleteBookConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(DeleteBookEvent));
        }

        public async Task Consume(ConsumeContext<DeleteBookEvent> context)
        {
            await _syncService.RemoveAsync(context.Message.Id);
        }
    }
}
