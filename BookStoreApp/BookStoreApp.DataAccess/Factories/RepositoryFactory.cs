using AutoMapper.Internal;
using BookStore.Application.Common.Models;
using BookStore.Domain.Primitives;
using BookStore.Mongo.Implementations;
using BookStore.Mongo.Infrastructure;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using BookStore.Persistance.Interfaces;
using BookStore.Persistance.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Services
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IDbConnection _connection;
        private readonly MongoSettings _mongoSettings;
        private object _commandRepository;
        private object _queryRepository;
        public RepositoryFactory(
            IDbConnection connection, 
            MongoSettings mongoSettings)
        {
            _commandRepository = new object();
            _queryRepository = new object();
            _connection = connection;
            _mongoSettings = mongoSettings;
        }
        public Task<TRepository> GetCommandRepository<TRepository, TEntity>()
            where TRepository : IRepository<TEntity>
            where TEntity : BaseModel
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes()
                 .FirstOrDefault(t => t.GetInterfaces().Contains(typeof(TRepository)));

            if (type is null)
            {
                throw new BookStoreException("Impossible to find instance type");
            }

            ConstructorInfo constructor = type?.GetConstructor(new Type[] { typeof(IDbConnection) });

            if(constructor is null)
            {
                throw new BookStoreException("Impossible to create repository instance");
            }

            _commandRepository = constructor?.Invoke(new object[] { _connection });
            return Task.FromResult((TRepository)_commandRepository );
        }

        public Task<IMongoRepository<TDocument>> GetQueryRepository<TDocument>()
            where TDocument : Document
        {
            var queryRepository = new MongoRepository<TDocument>(_mongoSettings);
            return Task.FromResult((IMongoRepository<TDocument>)queryRepository);
        }
    }
}
