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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IDbConnection _connection;
        public string TableName => "\"Authors\"";
        public AuthorRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Result> CreateAsync(Author entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(id, name, surname) " +
                         "VALUES (@Id, @Name, @Surname)";;

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

        public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Author>($"SELECT * FROM {TableName}", cancellationToken);
        }

        public async Task<Author> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<Author>($"SELECT * FROM {TableName} WHERE id=@Id", new { Id = id });
        }

        public async Task<Result> UpdateAsync(Author entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET name=@Name, surname=@Surname " +
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
    }
}
