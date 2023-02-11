using BookStore.Application.Common.Validation;
using BookStore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrder : IRequest<Result>
    {
        public Guid Id { get; set; }
        public decimal? Total { get; init; }
        public decimal? Discount { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime? ClosingDate { get; init; }
        public OrderStatus? Status { get; init; }
    }
}
