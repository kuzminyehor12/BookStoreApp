using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.CreateBook
{
    public class CreateBookConsumer : IConsumer<CreateBookEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public CreateBookConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(CreateBookEvent));
        }

        public async Task Consume(ConsumeContext<CreateBookEvent> context)
        {
            await _syncService.AddAsync(context.Message);
        }
    }
}
