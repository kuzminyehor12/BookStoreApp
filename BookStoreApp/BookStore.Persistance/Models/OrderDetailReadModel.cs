using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Models;
using BookStore.Domain.Models;
using BookStore.Mongo.Infrastructure;

namespace BookStore.Mongo.Models
{
    [DocumentCollection("details")]
    public class OrderDetailReadModel : Document, IMapWith<OrderDetailViewModel>
    {
        public BookReadModel? Book { get; set; }
        public int Amount { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<OrderDetailViewModel, OrderDetailReadModel>()
                .ForMember(odrm => odrm.Book, mem => mem
                    .MapFrom(odvm => odvm.Book))
                .ReverseMap();
        }
    }
}
