using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using BookStore.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.ViewModels
{
    public class AuthorViewModel : BaseModel, IMapWith<Author>
    {
        public string FullName { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<Author, AuthorViewModel>()
                .ForMember(avm => avm.FullName, mem => mem
                    .MapFrom(a => a.Surname + " " + a.Name));
        }
    }
}
