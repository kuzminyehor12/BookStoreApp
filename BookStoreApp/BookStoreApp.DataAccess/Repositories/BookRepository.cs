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
        public string TableName => "Books";
        public BookRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> CreateAsync(Book entity, CancellationToken cancellationToken)
        {
            var command = $"INSERT INTO {TableName}(isbn, title, description, amount_on_stock, price, author_id) " +
                          "VALUES (@isbn, @title, @desciption, @amount_on_stock, @price, @author_id)";

            var parameters = new DynamicParameters();
            parameters.Add("isbn", entity.ISBN, DbType.String);
            parameters.Add("title", entity.Title, DbType.String);
            parameters.Add("description", entity.Description, DbType.String);
            parameters.Add("amount_on_stock", entity.AmountOnStock, DbType.Int32);
            parameters.Add("price", entity.Price, DbType.Decimal);
            parameters.Add("author_id", entity.AuthorId, DbType.Guid);

            var affected = await _connection.ExecuteAsync(command, cancellationToken);
            return affected > 0;
        }


        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET is_deleted=true WHERE id=@Id";
            var affected = await _connection.ExecuteAsync(command, new { Id = id });
            return affected > 0;
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


        public async Task<bool> UpdateAsync(Book entity, CancellationToken cancellationToken)
        {
            var command = $"UPDATE {TableName} SET isbn=@isbn, title=@title, description=@desciption, " +
                          "amount_on_stock=@amount_on_stock, price=@price, author_id=@author_id)";

            var parameters = new DynamicParameters();
            parameters.Add("isbn", entity.ISBN, DbType.String);
            parameters.Add("title", entity.Title, DbType.String);
            parameters.Add("description", entity.Description, DbType.String);
            parameters.Add("amount_on_stock", entity.AmountOnStock, DbType.Int32);
            parameters.Add("price", entity.Price, DbType.Decimal);
            parameters.Add("author_id", entity.AuthorId, DbType.Guid);

            var affected = await _connection.ExecuteAsync(command, cancellationToken);
            return affected > 0;
        }
    }
}
