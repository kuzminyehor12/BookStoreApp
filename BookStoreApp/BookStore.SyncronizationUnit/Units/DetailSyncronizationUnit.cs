using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;
using BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount;
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
    public class DetailSyncronizationUnit : ISyncronizationUnit
    {
        private readonly IMongoRepository<OrderDetailReadModel> _readRepository;
        private readonly IMongoRepository<BookReadModel> _bookReadRepository;

        public DetailSyncronizationUnit(
            IMongoRepository<OrderDetailReadModel> readRepository, IMongoRepository<BookReadModel> bookReadRepository)
        {
            _readRepository = readRepository;
            _bookReadRepository = bookReadRepository;
        }

        public async Task<Result> AddAsync(object model, CancellationToken cancellationToken = default)
        {
            if (model is not AddOrderDetailEvent @event || @event.Result.IsFailure)
            {
                return Result.Failure();
            }

            var book = await _bookReadRepository.FindByIdAsync(@event.BookId);

            var newDetail = new OrderDetailReadModel
            {
                Amount = @event.Amount ?? default,
                Book = book
            };

            try
            {
                await _readRepository.InsertOneAsync(newDetail);
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
            if (model is not ChangeOrderDetailAmountEvent @event || @event.Result.IsFailure)
            {
                return Result.Failure();
            }

            var detail = await _readRepository.FindByIdAsync(@event.Id);

            var newDetail = new OrderDetailReadModel
            {
                Id = detail.Id,
                Amount = @event.Amount ?? default,
                Book = detail.Book
            };

            try
            {
                await _readRepository.InsertOneAsync(newDetail);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
