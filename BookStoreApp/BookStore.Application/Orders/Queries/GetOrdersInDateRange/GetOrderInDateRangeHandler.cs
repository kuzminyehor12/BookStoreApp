using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Queries.GetOrdersInDateRange
{
    public class GetOrderInDateRangeHandler : IRequestHandler<GetOrdersInDateRange, IEnumerable<OrderViewModel>>
    {
        private IOrderService _service;
        public GetOrderInDateRangeHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersInDateRange request, CancellationToken cancellationToken)
        {
            return await _service.GetInDateRangeAsync(request.StartDate, request.EndDate, cancellationToken);
        }
    }
}
