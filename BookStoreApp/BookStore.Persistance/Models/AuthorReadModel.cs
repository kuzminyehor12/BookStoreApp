using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using BookStore.Mongo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Models
{
    [DocumentCollection("authors")]
    public class AuthorReadModel : Document, IMapWith<AuthorReadModel>
    {
        public string FullName { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<AuthorReadModel, AuthorViewModel>()
                .ReverseMap();
        }
    }
}
