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
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _connection;
        public string TableName => "\"Orders\"";
        public OrderRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Result> CreateAsync(Order entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(creation_date, closing_date, total, status) " +
                          "VALUES (@CreationDate, @ClosingDate, @Total, @Status)";

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
            var command = $"UPDATE {TableName} SET is_deleted=true WHERE id=@Id";
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

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Order>("SELECT * FROM Orders", cancellationToken);
        }

        public async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<Order>("SELECT * FROM Orders WHERE id=@Id", new { Id = id });
        }

        public async Task<IEnumerable<Order>> GetInDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Order>("SELECT * FROM Orders " +
                "WHERE creation_date => @CreationDate AND closing_date <= @ClosingDate", new { CreationDate = startDate, ClosingDate = endDate });
        }

        public async Task<Result> UpdateAsync(Order entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET creation_date=@CreationDate, closing_date=@ClosingDate," +
                          "total=@Total, status=@Status) WHERE id=@Id";

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
    }
}
