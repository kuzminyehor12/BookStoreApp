using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorConsumer : IConsumer<UpdateAuthorEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public UpdateAuthorConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(UpdateAuthorEvent));
        }

        public async Task Consume(ConsumeContext<UpdateAuthorEvent> context)
        {
            await _syncService.ReplaceAsync(context.Message);
        }
    }
}
