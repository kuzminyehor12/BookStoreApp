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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorService(IUnitOfWork unifOfWork, IMapper mapper)
        {
            _unitOfWork = unifOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Author model, CancellationToken cancellationToken)
        {
            await _unitOfWork.AuthorRepository.CreateAsync(model, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.AuthorRepository.DeleteAsync(id, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Author model, CancellationToken cancellationToken)
        {
            await _unitOfWork.AuthorRepository.UpdateAsync(model, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.AuthorRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AuthorViewModel>>(entities);
        }

        public async Task<AuthorViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.AuthorRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AuthorViewModel>(entity);
        }
    }
}
