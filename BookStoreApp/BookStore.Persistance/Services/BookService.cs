using AutoMapper;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.DeleteBook;
using BookStore.Application.Books.Commands.UpdateBook;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public BookService(
            IBookRepository bookRepository, 
            IMapper mapper,
            IEventBus eventBus)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Result> CreateAsync(Book model, CancellationToken cancellationToken = default)
        {
            var result = await _bookRepository.CreateAsync(model, cancellationToken);

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
            var result = await _bookRepository.DeleteAsync(id, cancellationToken);

            await _eventBus.PublishAsync(new DeleteBookEvent
            {
                Id = id,
                Result = result.ToString()
            }, cancellationToken);

            return result; 
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _bookRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<IEnumerable<BookViewModel>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default)
        {
            var entities = await _bookRepository.GetByAuthorIdAsync(authorId, cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<BookViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _bookRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<BookViewModel> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            var entity = await _bookRepository.GetByIsbnAsync(isbn, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<IEnumerable<BookViewModel>> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            var entities = await _bookRepository.GetByTitleAsync(title, cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<Result> UpdateAsync(Book model, CancellationToken cancellationToken = default)
        {
            var result = await _bookRepository.UpdateAsync(model, cancellationToken);

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
