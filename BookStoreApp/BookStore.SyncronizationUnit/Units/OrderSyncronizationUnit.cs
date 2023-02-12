using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Validation;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Commands.UpdateOrder;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
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
    public  class OrderSyncronizationUnit : ISyncronizationUnit
    {
        private readonly IMongoRepository<OrderReadModel> _readRepository;
        private readonly IMongoRepository<BookReadModel> _bookReadRepository;
        private readonly IMongoRepository<OrderDetailReadModel> _detailRepository;

        public OrderSyncronizationUnit(
            IMongoRepository<OrderReadModel> readRepository,
            IMongoRepository<OrderDetailReadModel> detailRepository,
            IMongoRepository<BookReadModel> bookReadRepository)
        {
            _readRepository = readRepository;
            _detailRepository = detailRepository;
            _bookReadRepository = bookReadRepository;
        }

        public async Task<Result> AddAsync(object model, CancellationToken cancellationToken = default)
        {
            if (model is not CreateOrderEvent @event)
            {
                return Result.Failure();
            }

            var details = await _detailRepository.FilterBy(d => d.Id == @event.Id);
            var books = await _bookReadRepository.ToListAsync();

            foreach (var detail in details)
            {
                detail.Book = books.FirstOrDefault(b => b.Id == detail.Book.Id);
            }

            var newOrder = new OrderReadModel
            {
                Id = @event.Id,
                CreationDate = @event.CreationDate,
                Status = OrderStatus.Open,
                Discount = @event.Discount ?? default,
                OrderDetails = details.ToArray(),
                IsDeleted = false
            };

            try
            {
                await _readRepository.InsertOneAsync(newOrder);
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
                var order = await _readRepository.FindByIdAsync(id);

                var newOrder = new OrderReadModel
                {
                    Id = id,
                    OrderDetails = order.OrderDetails,
                    CreationDate = order.CreationDate,
                    ClosingDate = order.ClosingDate,
                    Status = order.Status,
                    Discount = order.Discount,
                    IsDeleted = true
                };

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> ReplaceAsync(object model, CancellationToken cancellationToken = default)
        {
            if (model is not UpdateOrderEvent @event)
            {
                return Result.Failure();
            }

            var order = await _readRepository.FindByIdAsync(@event.Id);

            var newOrder = new OrderReadModel
            {
                Id = @event.Id,
                OrderDetails = order.OrderDetails,
                CreationDate = @event.CreationDate,
                ClosingDate = @event.ClosingDate,
                Status = @event.Status ?? OrderStatus.Shipped,
                Discount = @event.Discount ?? default,
                IsDeleted = false
            };

            try
            {
                await _readRepository.ReplaceOneAsync(newOrder);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
