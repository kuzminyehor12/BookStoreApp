using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using BookStore.Persistance.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unifOfWork, IMapper mapper)
        {
            _unitOfWork = unifOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Book model, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.CreateAsync(model, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.DeleteAsync(id, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.BookRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<IEnumerable<BookViewModel>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.BookRepository.GetByAuthorIdAsync(authorId, cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<BookViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.BookRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<BookViewModel> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.BookRepository.GetByIsbnAsync(isbn, cancellationToken);
            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<IEnumerable<BookViewModel>> GetByTitleAsync(string title, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.BookRepository.GetByTitleAsync(title, cancellationToken);
            return _mapper.Map<IEnumerable<BookViewModel>>(entities);
        }

        public async Task<bool> UpdateAsync(Book model, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.UpdateAsync(model, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
