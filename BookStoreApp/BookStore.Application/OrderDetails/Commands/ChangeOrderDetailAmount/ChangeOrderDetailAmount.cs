using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount
{
    public class ChangeOrderDetailAmount : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int? Amount { get; set; }
    }
}
