using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class OrderDetail : BaseModel
    {
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }
}
