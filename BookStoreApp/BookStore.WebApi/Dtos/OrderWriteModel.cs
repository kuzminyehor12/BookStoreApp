using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Commands.UpdateOrder;

namespace BookStore.WebApi.Dtos
{
    public class OrderWriteModel : IMapWith<CreateOrder>, IMapWith<UpdateOrder>
    {
        public decimal? Total { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<OrderWriteModel, CreateOrder>();
            profile.CreateMap<OrderWriteModel, UpdateOrder>();
        }
    }
}
