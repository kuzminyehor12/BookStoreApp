using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class BookViewModel : IMapWith<Book>
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AmountOnStock { get; set; }
        public decimal Price { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<Book, BookViewModel>();
        }
    }
}
