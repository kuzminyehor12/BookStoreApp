using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using BookStore.Persistance.Interfaces;
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
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Author model, CancellationToken cancellationToken)
        {
            return await _authorRepository.CreateAsync(model, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _authorRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Author model, CancellationToken cancellationToken)
        {
            return await _authorRepository.UpdateAsync(model, cancellationToken);
        }


        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _authorRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AuthorViewModel>>(entities);
        }

        public async Task<AuthorViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _authorRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AuthorViewModel>(entity);
        }
    }
}
