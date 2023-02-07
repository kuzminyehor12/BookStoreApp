using BookStore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrder : IRequest<bool>
    {
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
