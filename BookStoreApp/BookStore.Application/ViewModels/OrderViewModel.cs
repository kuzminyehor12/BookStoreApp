using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class OrderViewModel : IMapWith<Order>
    {
        public decimal Total { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Status { get; set; }

        public void UseMap(Profile profile)
        {
            profile.CreateMap<Order, OrderViewModel>()
                .ForMember(ovm => ovm.Status, mem => mem
                    .MapFrom(o => o.GetStatus()));
        }
    }
}
