using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Queries.GetOrdersInDateRange
{
    public class GetOrdersInDateRange : IRequest<IEnumerable<OrderViewModel>>
    {
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
    }
}
