using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Interfaces
{
    public interface IDetailRepository : IRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(Guid orderId, CancellationToken cancellationToken);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByBookId(Guid bookId, CancellationToken cancellationToken);
    }
}
