using BookStore.Application.Common.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorConsumer : IConsumer<CreateAuthorEvent>
    {
        private readonly ISyncronizationUnit _syncService;

        public CreateAuthorConsumer(ISyncronizationUnitFactory syncServiceFactory)
        {
            _syncService = syncServiceFactory.Create(typeof(CreateAuthorEvent));
        }

        public async Task Consume(ConsumeContext<CreateAuthorEvent> context)
        {
            await _syncService.AddAsync(context.Message);
        }
    }
}
