using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Book : BaseModel
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AmountOnStock { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
