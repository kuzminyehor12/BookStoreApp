using AutoMapper;
using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using BookStore.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.SyncronizationUnit.Units
{
    public class AuthorSyncronizationUnit : ISyncronizationUnit
    {
        private readonly IMongoRepository<AuthorReadModel> _readRepository;

        public AuthorSyncronizationUnit(
            IMongoRepository<AuthorReadModel> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Result> AddAsync(object model, CancellationToken cancellationToken = default)
        {
            if (model is not CreateAuthorEvent @event || @event.Result.IsFailure)
            {
                return Result.Failure();
            }

            var newAuthor = new AuthorReadModel
            {
                Id = @event.Id,
                FullName = @event.Name + " " + @event.Surname
            };

            try
            {
                await _readRepository.InsertOneAsync(newAuthor);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _readRepository.DeleteOneAsync(id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> ReplaceAsync(object model, CancellationToken cancellationToken = default)
        {
            if (model is not UpdateAuthorEvent @event || @event.Result.IsFailure)
            {
                return Result.Failure();
            }

            var newAuthor = new AuthorReadModel
            {
                Id = @event.Id,
                FullName = @event.Name + " " + @event.Surname
            };

            try
            {
                await _readRepository.ReplaceOneAsync(newAuthor);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
