using BookStore.Mongo.Infrastructure;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Implementations
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : Document
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(MongoSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        public string GetCollectionName(Type documentType)
        {
            return ((DocumentCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(DocumentCollectionAttribute), true).FirstOrDefault())?.CollectionName;
        }

        public async Task<IEnumerable<TDocument>> ToListAsync()
        {
            return await _collection
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filter)
        {
            return await _collection
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<TProjected>> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjected>> projection)
        {
            return await _collection
                .Find(filter)
                .Project(projection)
                .ToListAsync();
        }

        public async Task<TDocument> FindByIdAsync(Guid id)
        {
            return await Task.Run(() => _collection.Find(doc => doc.Id == id).SingleOrDefault());
        }

        public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await Task.Run(() => _collection.Find(filterExpression)
                        .FirstOrDefaultAsync());
        }

        public async Task ExecuteQueryAsync(string query)
        {
            var jsonCommand = new JsonCommand<BsonDocument>(query);
            await _collection.Database.RunCommandAsync(jsonCommand);
        }

        public async Task InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task DeleteOneAsync(Guid id)
        {
            var definition = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.DeleteOneAsync(definition);
        }

        public async Task ReplaceOneAsync(TDocument document)
        {
            var definition = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.ReplaceOneAsync(definition, document);
        }
    }
}
