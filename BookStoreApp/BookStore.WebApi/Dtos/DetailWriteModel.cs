using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;

namespace BookStore.WebApi.Dtos
{
    public class DetailWriteModel : IMapWith<AddOrderDetail>
    {
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public int? Amount { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<DetailWriteModel, AddOrderDetail>();
        }
    }
}
