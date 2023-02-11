using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Queries.GetOrderDetailsByBookId
{
    public class GetOrderDetailsByBookId : IRequest<IEnumerable<OrderDetailViewModel>>
    {
        public Guid BookId { get; set; }
    }
}
