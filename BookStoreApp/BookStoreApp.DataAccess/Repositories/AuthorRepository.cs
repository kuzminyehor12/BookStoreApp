using BookStore.Domain.Models;
using BookStore.Persistance.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookStoreApp.DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IDbConnection _connection;
        public string TableName => "Authors";
        public AuthorRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> CreateAsync(Author entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(name, surname) " +
                         "VALUES (@name, @surname)";

            var parameters = new DynamicParameters();
            parameters.Add("name", entity.Name, DbType.String);
            parameters.Add("surname", entity.Surname, DbType.String);

            var affected = await _connection.ExecuteAsync(command, cancellationToken);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = $"DELETE FROM {TableName}" +
                        "WHERE id=@Id";

            var affected = await _connection.ExecuteAsync(command, new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Author>($"SELECT * FROM {TableName}", cancellationToken);
        }

        public async Task<Author> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<Author>($"SELECT * FROM {TableName} WHERE id=@Id", new { Id = id });
        }

        public async Task<bool> UpdateAsync(Author entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET name=@name, surname=@surname";

            var parameters = new DynamicParameters();
            parameters.Add("name", entity.Name, DbType.String);
            parameters.Add("surname", entity.Surname, DbType.String);

            var affected = await _connection.ExecuteAsync(command, cancellationToken);
            return affected > 0;
        }
    }
}
