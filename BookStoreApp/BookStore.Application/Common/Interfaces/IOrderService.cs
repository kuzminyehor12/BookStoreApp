using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IOrderService : IService<Order, OrderViewModel>
    {
        Task<IEnumerable<OrderViewModel>> GetInDateRangeAsync(DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken);
        Task<bool> AddDetailAsync(OrderDetail detail, CancellationToken cancellationToken);
        Task<bool> RemoveDetailAsync(Guid detailId, CancellationToken cancellationToken);
        Task<bool> ChangeDetailAmountAsync(Guid detailId, int newAmount, CancellationToken cancellationToken);
        Task<IEnumerable<OrderDetailViewModel>> GetDetailsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<IEnumerable<OrderDetailViewModel>> GetDetailsByBookIdAsync(Guid bookId, CancellationToken cancellationToken);
    }
}
