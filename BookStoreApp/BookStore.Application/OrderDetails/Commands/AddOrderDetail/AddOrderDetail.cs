using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.AddOrderDetail
{
    public class AddOrderDetail : IRequest<bool>
    {
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }
}
