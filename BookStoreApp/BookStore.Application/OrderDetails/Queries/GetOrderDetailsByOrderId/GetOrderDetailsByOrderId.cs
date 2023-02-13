using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Queries.GetOrderDetailsByOrderId
{
    public class GetOrderDetailsByOrderId : IRequest<IEnumerable<OrderDetailViewModel>>
    {
        public Guid OrderId { get; init; }
    }
}
