using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrder, Result>
    {
        private IOrderService _service;
        public CreateOrderHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Discount = request.Discount ?? default,
                CreationDate = request.CreationDate,
                ClosingDate = null,
                Status = OrderStatus.Open
            };

            return await _service.CreateAsync(order, cancellationToken);
        }
    }
}
