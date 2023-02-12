using AutoMapper;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.UpdateBook;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using BookStore.Persistance.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.SyncronizationUnit.Units
{
    public class BookSyncronizationUnit : ISyncronizationUnit
    {
        private readonly IMongoRepository<BookReadModel> _readRepository;
        private readonly IMongoRepository<AuthorReadModel> _authorRepository;

        public BookSyncronizationUnit(
            IMongoRepository<BookReadModel> readRepository,
            IMongoRepository<AuthorReadModel> authorRepository)
        {
            _readRepository = readRepository;
            _authorRepository = authorRepository;
        }

        public async Task<Result> AddAsync(object model, CancellationToken cancellationToken = default)
        {
            if(model is not CreateBookEvent @event)
            {
                return Result.Failure();
            }

            var author = await _authorRepository.FindByIdAsync(@event.AuthorId ?? Guid.Empty);

            var newBook = new BookReadModel
            {
                Id = @event.Id,
                ISBN = @event.ISBN,
                Title = @event.Title,
                Description = @event.Description,
                AmountOnStock = @event.AmountOnStock ?? default,
                Price = @event.Price ?? default,
                IsDeleted = false,
                Author = author
            };

            try
            {
                await _readRepository.InsertOneAsync(newBook);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _readRepository.FindByIdAsync(id);

            var newBook = new BookReadModel
            {
                Id = id,
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                AmountOnStock = book.AmountOnStock,
                Price = book.Price,
                Author = book.Author,
                IsDeleted = true
            };

            try
            {
                await _readRepository.ReplaceOneAsync(newBook);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> ReplaceAsync(object model, CancellationToken cancellationToken = default)
        {
            if (model is not UpdateBookEvent @event)
            {
                return Result.Failure();
            }

            var author = await _authorRepository.FindByIdAsync(@event.AuthorId ?? Guid.Empty);

            var newBook = new BookReadModel
            {
                Id = @event.Id,
                ISBN = @event.ISBN,
                Title = @event.Title,
                Description = @event.Description,
                AmountOnStock = @event.AmountOnStock ?? default,
                Price = @event.Price ?? default,
                Author = author,
                IsDeleted = false
            };

            try
            {
                await _readRepository.ReplaceOneAsync(newBook);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
