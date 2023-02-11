using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Models;
using BookStore.Mongo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Models
{
    [DocumentCollection("books")]
    public class BookReadModel : Document, IMapWith<Book>
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AmountOnStock { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public AuthorReadModel Author { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<Book, BookReadModel>();
        }
    }
}
