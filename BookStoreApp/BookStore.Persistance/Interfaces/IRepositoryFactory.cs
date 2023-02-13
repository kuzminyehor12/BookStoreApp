using BookStore.Application.Common.Models;
using BookStore.Domain.Primitives;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Interfaces
{
    public interface IRepositoryFactory
    {
        Task<TRepository> GetCommandRepository<TRepository, TEntity>() 
            where TRepository : IRepository<TEntity>
            where TEntity : BaseModel;
        Task<IMongoRepository<TDocument>> GetQueryRepository<TDocument>()
            where TDocument : Document;
    }
}
