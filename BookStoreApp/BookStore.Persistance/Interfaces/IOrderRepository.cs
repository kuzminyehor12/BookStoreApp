using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetInDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
