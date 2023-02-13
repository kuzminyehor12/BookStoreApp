using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Models;
using BookStore.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Models
{
    public class OrderDetailViewModel : BaseModel, IMapWith<Order>
    {
        public BookViewModel? Book { get; set; }
        public int Amount { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<OrderDetail, OrderDetailViewModel>();
        }
    }
}
