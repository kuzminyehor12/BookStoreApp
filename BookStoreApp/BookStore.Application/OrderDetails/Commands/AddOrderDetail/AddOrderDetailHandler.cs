using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.AddOrderDetail
{
    public class AddOrderDetailHandler : IRequestHandler<AddOrderDetail, Result>
    {
        private readonly IOrderService _service;
        public AddOrderDetailHandler(IOrderService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(AddOrderDetail request, CancellationToken cancellationToken)
        {
            var detail = new OrderDetail
            {
                BookId = request.BookId,
                OrderId = request.OrderId,
                Amount = request.Amount ?? default
            };

            return await _service.AddDetailAsync(detail, cancellationToken);
        }
    }
}
