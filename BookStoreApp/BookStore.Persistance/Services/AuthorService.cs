using AutoMapper;
using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
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
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly IRepositoryFactory _repositoryFactory;
        public AuthorService(
            IAuthorRepository authorRepository,
            IMapper mapper,
            IEventBus eventBus,
            IRepositoryFactory repositoryFactory)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _eventBus = eventBus;
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result> CreateAsync(Author model, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IAuthorRepository, Author>();
            var result = await repository.CreateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new CreateAuthorEvent
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Result = result.ToString()
            }, cancellationToken);
            
            return result;
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IAuthorRepository, Author>();
            var result = await repository.DeleteAsync(id, cancellationToken);
            
           await _eventBus.PublishAsync(new DeleteAuthorEvent
           {
               Id = id,
               Result = result.ToString()
           }, cancellationToken);
            
           return result;
        }

        public async Task<Result> UpdateAsync(Author model, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IAuthorRepository, Author>();
            var result = await repository.UpdateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new UpdateAuthorEvent
            {
                Id = model.Id,
                Surname = model.Surname,
                Name = model.Name,
                Result = result.ToString()
            }, cancellationToken);
            
            return result;
        }


        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<AuthorReadModel>();
            var entities = await repository.ToListAsync();
            return _mapper.Map<IEnumerable<AuthorViewModel>>(entities);
        }

        public async Task<AuthorViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<AuthorReadModel>();
            var entity = await repository.FindByIdAsync(id);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<AuthorViewModel>(entity);
        }
    }
}
