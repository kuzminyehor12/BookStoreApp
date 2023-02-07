using AutoMapper;
using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
using BookStore.Application.Interfaces;

namespace BookStore.WebApi.Dtos
{
    public class AuthorWriteModel : IMapWith<CreateAuthor>, IMapWith<UpdateAuthor>
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<AuthorWriteModel, CreateAuthor>();
            profile.CreateMap<AuthorWriteModel, UpdateAuthor>();
        }
    }
}
