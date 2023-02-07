using BookStore.Application.Interfaces;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;

namespace BookStore.WebApi.Dtos
{
    public class DetailWriteModel : IMapWith<AddOrderDetail>
    {
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }
}
