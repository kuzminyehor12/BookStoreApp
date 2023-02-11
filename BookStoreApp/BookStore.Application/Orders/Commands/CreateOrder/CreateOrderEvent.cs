using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderEvent : OrderEvent
    {
        public decimal? Discount { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
