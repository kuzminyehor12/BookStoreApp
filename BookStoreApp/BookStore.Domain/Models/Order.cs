using BookStore.Domain.Enums;
using BookStore.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Order : BaseModel
    {
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public string GetStatus()
        {
            switch (Status)
            {
                case OrderStatus.Open:
                    return "Open";
                case OrderStatus.Paid:
                    return "Paid";
                case OrderStatus.Shipped:
                    return "Shipped";
                default:
                    return "Closed";
            }
        }
    }
}
