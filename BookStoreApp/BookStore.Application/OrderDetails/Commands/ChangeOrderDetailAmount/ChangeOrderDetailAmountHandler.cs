using BookStore.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount
{
    public class ChangeOrderDetailAmountHandler : IRequestHandler<ChangeOrderDetailAmount, bool>
    {
        private readonly IOrderService _service;
        public ChangeOrderDetailAmountHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(ChangeOrderDetailAmount request, CancellationToken cancellationToken)
        {
            return await _service.ChangeDetailAmountAsync(request.Id, request.Amount ?? default, cancellationToken);
        }
    }
}
