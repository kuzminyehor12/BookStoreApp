using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Models
{
    public class OrderViewModel : BaseModel, IMapWith<Order>
    {
        public OrderDetailViewModel[]? OrderDetails { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => OrderDetails?.Sum(od => od.Book.Price * od.Amount) - Discount ?? default;
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<Order, OrderViewModel>();
        }
    }
}
