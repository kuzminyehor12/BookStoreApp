using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class OrderDetailViewModel
    {
        public string BookName { get; set; }
        public string Price { get; set; }
        public int Amount { get; set; }
    }
}
