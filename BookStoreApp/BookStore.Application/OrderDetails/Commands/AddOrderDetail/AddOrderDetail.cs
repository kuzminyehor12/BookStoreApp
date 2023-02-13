using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.AddOrderDetail
{
    public class AddOrderDetail : IRequest<Result>
    {
        public Guid BookId { get; init; }
        public Guid OrderId { get; init; }
        public int? Amount { get; init; }
    }
}
