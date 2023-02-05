using BookStore.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.RemoveOrderDetail
{
    public class RemoveOrderDetailHandler : IRequestHandler<RemoveOrderDetail, bool>
    {
        private readonly IOrderService _service;
        public RemoveOrderDetailHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(RemoveOrderDetail request, CancellationToken cancellationToken)
        {
            return await _service.RemoveDetailAsync(request.Id, cancellationToken);
        }
    }
}
