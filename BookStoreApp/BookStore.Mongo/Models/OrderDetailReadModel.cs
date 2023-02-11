using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Models;
using BookStore.Mongo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Models
{
    [DocumentCollection("details")]
    public class OrderDetailReadModel : Document, IMapWith<Order>
    {
        public BookReadModel Book { get; set; }
        public int Amount { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<OrderDetail, OrderDetailReadModel>()
                .ForMember(odrm => odrm.Book.Id, mem => mem
                    .MapFrom(od => od.BookId));
        }
    }
}
