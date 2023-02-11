using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Queries.GetOrderDetailsByBookId
{
    public class GetOrderDetailsByBookIdHandler : IRequestHandler<GetOrderDetailsByBookId, IEnumerable<OrderDetailViewModel>>
    {
        private readonly IOrderService _service;
        public GetOrderDetailsByBookIdHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> Handle(GetOrderDetailsByBookId request, CancellationToken cancellationToken)
        {
            return await _service.GetDetailsByBookIdAsync(request.BookId, cancellationToken);
        }
    }
}
