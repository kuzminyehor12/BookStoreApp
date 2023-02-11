using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Primitives;

namespace BookStore.Domain.Models
{
    public class Author : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
