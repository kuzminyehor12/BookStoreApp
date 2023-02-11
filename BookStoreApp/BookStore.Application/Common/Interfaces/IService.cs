using BookStore.Application.Common.Validation;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IService<TModel, TViewModel> 
           where TModel : BaseModel 
           where TViewModel : IMapWith<TModel>
    {
        Task<IEnumerable<TViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result> CreateAsync(TModel model, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(TModel model, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
