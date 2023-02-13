using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.RemoveOrderDetail
{
    public class RemoveOrderDetailHandler : IRequestHandler<RemoveOrderDetail, Result>
    {
        private readonly IOrderService _service;
        public RemoveOrderDetailHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(RemoveOrderDetail request, CancellationToken cancellationToken)
        {
            return await _service.RemoveDetailAsync(request.Id, cancellationToken);
        }
    }
}
