using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Mongo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Models
{
    [DocumentCollection("orders")]
    public class OrderReadModel : Document, IMapWith<OrderViewModel>
    {
        public OrderDetailReadModel[]? OrderDetails { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => (OrderDetails?.Sum(od => od.Book.Price * od.Amount) - Discount) ?? default;
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<OrderViewModel, OrderReadModel>()
                .ForMember(orm => orm.OrderDetails, mem => mem
                    .MapFrom(ovm => ovm.OrderDetails))
                .ReverseMap();
        }
    }
}
