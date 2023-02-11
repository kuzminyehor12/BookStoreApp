using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrder, bool>
    {
        private readonly IOrderService _service;
        public DeleteOrderHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteOrder request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
