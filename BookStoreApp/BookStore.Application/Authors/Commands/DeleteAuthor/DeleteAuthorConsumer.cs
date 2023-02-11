using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorConsumer : IConsumer<DeleteAuthorEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public DeleteAuthorConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(DeleteAuthorEvent));
        }

        public async Task Consume(ConsumeContext<DeleteAuthorEvent> context)
        {
            await _syncService.RemoveAsync(context.Message.Id);
        }
    }
}
