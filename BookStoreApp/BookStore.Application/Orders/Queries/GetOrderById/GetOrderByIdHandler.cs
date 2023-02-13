using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderById, OrderViewModel>
    {
        private readonly IOrderService _service;
        public GetOrderByIdHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<OrderViewModel> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
