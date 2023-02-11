using AutoMapper;
using BookStore.Application.Common.Interfaces;
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
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Book model, CancellationToken cancellationToken)
        {
            return await _bookRepository.CreateAsync(model, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _bookRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _bookRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<IEnumerable<BookViewModel>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken)
        {
            var entities = await _bookRepository.GetByAuthorIdAsync(authorId, cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<BookViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _bookRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<BookViewModel> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            var entity = await _bookRepository.GetByIsbnAsync(isbn, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<IEnumerable<BookViewModel>> GetByTitleAsync(string title, CancellationToken cancellationToken)
        {
            var entities = await _bookRepository.GetByTitleAsync(title, cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<bool> UpdateAsync(Book model, CancellationToken cancellationToken)
        {
            return await _bookRepository.UpdateAsync(model, cancellationToken);
        }
    }
}
