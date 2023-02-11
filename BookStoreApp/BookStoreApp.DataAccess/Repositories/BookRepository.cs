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
    public class BookRepository: IBookRepository
    {
        private readonly IDbConnection _connection;
        public string TableName => "\"Books\"";
        public BookRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Result> CreateAsync(Book entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(isbn, title, description, amount_on_stock, price, author_id) " +
                          "VALUES (@Isbn, @Title, @Description, @AmountOnStock, @Price, @AuthorId)";

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

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Book>($"SELECT * FROM {TableName}", cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Book>($"SELECT * FROM {TableName} WHERE author_id=@AuthorId", new { AuthorId = authorId });
        }

        public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<Book>($"SELECT * FROM {TableName} WHERE id=@Id", new { Id = id });
        }

        public async Task<Book> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<Book>($"SELECT * FROM {TableName} WHERE isbn=@Isbn", new { Isbn = isbn });
        }

        public async Task<IEnumerable<Book>> GetByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<Book>($"SELECT * FROM {TableName} WHERE title=@Title", new { Title = title });
        }


        public async Task<Result> UpdateAsync(Book entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET isbn=@Isbn, title=@Title, description=@Description, " +
                          "amount_on_stock=@AmountOnStock, price=@Price, author_id=@AuthorId " +
                          "WHERE id=@Id)";

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
