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
        Task<IEnumerable<TViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<TViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
