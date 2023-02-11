using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderEvent : DomainEvent
    {
        public decimal? Total { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
