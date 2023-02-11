using AutoMapper;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.UpdateBook;
using BookStore.Application.Common.Interfaces;

namespace BookStore.WebApi.Dtos
{
    public class BookWriteModel : IMapWith<CreateBook>, IMapWith<UpdateBook>
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AmountOnStock { get; set; }
        public decimal? Price { get; set; }
        public Guid? AuthorId { get; set; }
        public void UseMap(Profile profile)
        {
            profile.CreateMap<BookWriteModel, CreateBook>()
                .ForMember(b => b.ISBN, mem => mem
                    .MapFrom(cb => cb.Isbn));

            profile.CreateMap<BookWriteModel, UpdateBook>()
                 .ForMember(b => b.ISBN, mem => mem
                    .MapFrom(cb => cb.Isbn));
        }
    }
}
