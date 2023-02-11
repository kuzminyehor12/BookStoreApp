using BookStore.Application.Common.Validation;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Interfaces
{
    public interface IRepository<TEntity> where TEntity: BaseModel
    {
        public string TableName { get; }
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
