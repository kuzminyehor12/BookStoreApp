using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.ViewModels
{
    public class OrderDetailViewModel : IMapWith<OrderDetail>
    {
        public Guid Id { get; set; }
        public string? BookName { get; set; }
        public decimal BookPrice { get; set; }
        public int Amount { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<OrderDetail, OrderDetailViewModel>();
        }
    }
}
