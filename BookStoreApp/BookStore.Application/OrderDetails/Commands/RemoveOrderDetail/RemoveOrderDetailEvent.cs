using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.RemoveOrderDetail
{
    public class RemoveOrderDetailEvent : DomainEvent
    {
        public Guid Id { get; set; }
    }
}
