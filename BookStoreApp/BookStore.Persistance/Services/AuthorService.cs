using AutoMapper;
using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Messaging;
using BookStore.Application.Common.Validation;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
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
        public AuthorService(
            IAuthorRepository authorRepository, 
            IMapper mapper,
            IEventBus eventBus)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Result> CreateAsync(Author model, CancellationToken cancellationToken = default)
        {
            var result = await _authorRepository.CreateAsync(model, cancellationToken);

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
           var result = await _authorRepository.DeleteAsync(id, cancellationToken);
            
           await _eventBus.PublishAsync(new DeleteAuthorEvent
           {
               Id = id,
               Result = result.ToString()
           }, cancellationToken);
            
           return result;
        }

        public async Task<Result> UpdateAsync(Author model, CancellationToken cancellationToken = default)
        {
            var result = await _authorRepository.UpdateAsync(model, cancellationToken);

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
            var entities = await _authorRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AuthorViewModel>>(entities);
        }

        public async Task<AuthorViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _authorRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<AuthorViewModel>(entity);
        }
    }
}
