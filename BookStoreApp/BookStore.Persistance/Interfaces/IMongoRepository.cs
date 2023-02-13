using BookStore.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : class
    {
        Task<IEnumerable<TDocument>> ToListAsync();
        Task<IEnumerable<TDocument>> FilterBy(
            Expression<Func<TDocument, bool>> filter);
        Task<IEnumerable<TProjected>> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjected>> projection);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filter);
        Task<TDocument> FindByIdAsync(Guid id);
        Task InsertOneAsync(TDocument document);
        Task DeleteOneAsync(Guid id);
        Task ReplaceOneAsync(TDocument document);
        string GetCollectionName(Type documentType);
    }
}
