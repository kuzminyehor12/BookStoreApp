using BookStore.Application.Common.Validation;
using BookStore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrder : IRequest<Result>
    {
        public decimal? Total { get; init; }
        public decimal? Discount { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
