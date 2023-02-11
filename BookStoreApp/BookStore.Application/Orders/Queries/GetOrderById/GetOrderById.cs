using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Queries.GetOrderById
{
    public class GetOrderById : IRequest<OrderViewModel>
    {
        public Guid Id { get; set; }
    }
}
