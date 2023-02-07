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
        public string TableName => "Orders";
        public OrderRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> CreateAsync(Order entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(creation_date, closing_date, total, status) " +
                          "VALUES (@creation_date, @closing_date, @total, @status)";

            var parameters = new DynamicParameters();
            parameters.Add("creation_date", entity.CreationDate, DbType.DateTime);
            parameters.Add("closing_date", entity.ClosingDate, DbType.DateTime);
            parameters.Add("total", entity.Total, DbType.Decimal);
            parameters.Add("status", entity.Status, DbType.Byte);

            var affected = await _connection.ExecuteAsync(command, cancellationToken);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET is_deleted=true WHERE id=@Id";
            var affected = await _connection.ExecuteAsync(command, new { Id = id });
            return affected > 0;
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

        public async Task<bool> UpdateAsync(Order entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET creation_date=@creation_date, closing_date=@closing_date," +
                          "total=@total, status=@status)";

            var parameters = new DynamicParameters();
            parameters.Add("creation_date", entity.CreationDate, DbType.DateTime);
            parameters.Add("closing_date", entity.ClosingDate, DbType.DateTime);
            parameters.Add("total", entity.Total, DbType.Decimal);
            parameters.Add("status", entity.Status, DbType.Byte);

            var affected = await _connection.ExecuteAsync(command, cancellationToken);
            return affected > 0;
        }
    }
}
