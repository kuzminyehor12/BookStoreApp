using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Queries.GetOrderDetailsByOrderId
{
    public class GetOrderDetailsByOrderIdHandler : IRequestHandler<GetOrderDetailsByOrderId, IEnumerable<OrderDetailViewModel>>
    {
        private readonly IOrderService _service;
        public GetOrderDetailsByOrderIdHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> Handle(GetOrderDetailsByOrderId request, CancellationToken cancellationToken)
        {
            return await _service.GetDetailsByOrderIdAsync(request.OrderId, cancellationToken);
        }
    }
}
