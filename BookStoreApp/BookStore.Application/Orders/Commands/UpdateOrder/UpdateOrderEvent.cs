using BookStore.Application.Common.Messaging;
using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderEvent : OrderEvent
    {
        public decimal? Discount { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime? ClosingDate { get; init; }
        public OrderStatus? Status { get; init; }
    }
}
