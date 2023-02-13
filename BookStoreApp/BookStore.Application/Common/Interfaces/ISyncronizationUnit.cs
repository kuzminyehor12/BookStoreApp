using BookStore.Application.Common.Messaging;
using BookStore.Application.Common.Validation;
using BookStore.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface ISyncronizationUnit
    {
        Task<Result> AddAsync(object model, CancellationToken cancellationToken = default);
        Task<Result> ReplaceAsync(object model, CancellationToken cancellationToken = default);
        Task<Result> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
