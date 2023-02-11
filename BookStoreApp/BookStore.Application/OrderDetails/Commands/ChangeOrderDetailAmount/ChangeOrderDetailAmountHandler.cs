using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount
{
    public class ChangeOrderDetailAmountHandler : IRequestHandler<ChangeOrderDetailAmount, Result>
    {
        private readonly IOrderService _service;
        public ChangeOrderDetailAmountHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(ChangeOrderDetailAmount request, CancellationToken cancellationToken)
        {
            return await _service.ChangeDetailAmountAsync(request.Id, request.Amount ?? default, cancellationToken);
        }
    }
}
