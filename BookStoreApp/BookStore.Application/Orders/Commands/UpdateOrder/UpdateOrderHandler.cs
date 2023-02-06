﻿using BookStore.Application.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrder, bool>
    {
        private IOrderService _service;
        public UpdateOrderHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Total = request.Total,
                CreationDate = request.CreationDate,
                ClosingDate = request.ClosingDate,
                Status = request.Status
            };

            return await _service.UpdateAsync(order, cancellationToken);
        }
    }
}