using BookStore.Application.Common.Validation;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Persistance.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.DataAccess.Repositories
{
    public class DetailRepository : IDetailRepository
    {
        private readonly IDbConnection _connection;
        public string TableName => "\"OrderDetails\"";
        public DetailRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Result> CreateAsync(OrderDetail entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(id, book_id, order_id, amount) " +
                        "VALUES (@Id, @BookId, @OrderId, @Amount)";

            try
            {
                await _connection.ExecuteAsync(command, entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
            
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = $"DELETE FROM {TableName}" +
                           "WHERE id=@Id";

            try
            {
                await _connection.ExecuteAsync(command, new { Id = id });
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<OrderDetail>($"SELECT * FROM {TableName}", cancellationToken);
        }

        public async Task<OrderDetail> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<OrderDetail>($"SELECT * FROM {TableName} WHERE id=@Id", new { Id = id });
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(Guid orderId, CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<OrderDetail>($"SELECT * FROM {TableName} WHERE order_id=@OrderId", new { OrderId = orderId });
        }

        public async Task<Result> UpdateAsync(OrderDetail entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET book_id=@BookId, order_id=@OrderId, amount=@Amount " +
                           "WHERE id=@Id";

            try
            {
                await _connection.ExecuteAsync(command, entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByBookId(Guid bookId, CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<OrderDetail>($"SELECT * FROM {TableName} WHERE order_id=@BookId", new { BookId = bookId });
        }
    }
}
