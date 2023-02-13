using AutoMapper;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.DeleteBook;
using BookStore.Application.Books.Commands.UpdateBook;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Messaging;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.Validation;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using BookStore.Persistance.Interfaces;
using BookStore.Persistance.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public BookService(
            IMapper mapper,
            IEventBus eventBus,
            IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result> CreateAsync(Book model, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IBookRepository, Book>();
            var result = await repository.CreateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new CreateBookEvent
            {
                Id = model.Id,
                ISBN = model.ISBN,
                Title = model.Title,
                AmountOnStock = model.AmountOnStock,
                Price = model.Price,
                Description = model.Description,
                AuthorId = model.AuthorId,
                Result = result.ToString()
            }, cancellationToken);
            
            return result;
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IBookRepository, Book>();
            var result = await repository.DeleteAsync(id, cancellationToken);

            await _eventBus.PublishAsync(new DeleteBookEvent
            {
                Id = id,
                Result = result.ToString()
            }, cancellationToken);

            return result; 
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<BookReadModel>();
            var entities = await repository.ToListAsync();
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<IEnumerable<BookViewModel>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<BookReadModel>();
            var entities = await repository.FilterBy(b => b.Author.Id == authorId);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<BookViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<BookReadModel>();
            var entity = await repository.FindByIdAsync(id);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<BookViewModel> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<BookReadModel>();
            var entity = await repository.FindOneAsync(b => b.ISBN == isbn);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<IEnumerable<BookViewModel>> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<BookReadModel>();
            var entities = await repository.FilterBy(b => b.Title == title);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<Result> UpdateAsync(Book model, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IBookRepository, Book>();
            var result = await repository.UpdateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new UpdateBookEvent
            {
                Id = model.Id,
                ISBN = model.ISBN,
                Title = model.Title,
                AmountOnStock = model.AmountOnStock,
                Price = model.Price,
                Description = model.Description,
                AuthorId = model.AuthorId,
                Result = result.ToString()
            }, cancellationToken);
            
            return result;
        }
    }
}
