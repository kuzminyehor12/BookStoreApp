using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, IEnumerable<OrderViewModel>>
    {
        private readonly IOrderService _service;
        public GetAllOrdersHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
