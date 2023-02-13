using BookStore.Application.Common.Models;
using BookStore.Application.Common.Validation;
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
        Task<IEnumerable<OrderViewModel>> GetInDateRangeAsync(DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
        Task<Result> AddDetailAsync(OrderDetail detail, CancellationToken cancellationToken = default);
        Task<Result> RemoveDetailAsync(Guid detailId, CancellationToken cancellationToken = default);
        Task<Result> ChangeDetailAmountAsync(Guid detailId, int newAmount, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDetailViewModel>> GetDetailsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDetailViewModel>> GetDetailsByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    }
}
