using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrders : IRequest<IEnumerable<OrderViewModel>>
    {
        
    }
}
