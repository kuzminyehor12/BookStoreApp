using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrder : IRequest<Result>
    {
        public Guid Id { get; init; }
    }
}
