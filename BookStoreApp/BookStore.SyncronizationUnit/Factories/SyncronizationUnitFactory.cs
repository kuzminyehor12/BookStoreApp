using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Messaging;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using BookStore.Persistance.Interfaces;
using BookStore.SyncronizationUnit.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.SyncronizationUnit.Factories
{
    public class SyncronizationUnitFactory : ISyncronizationUnitFactory
    {
        private readonly IMongoRepository<BookReadModel> _bookReadRepository;
        private readonly IMongoRepository<AuthorReadModel> _authorReadRepository;
        private readonly IMongoRepository<OrderDetailReadModel> _orderDetailReadRepository;
        private readonly IMongoRepository<OrderReadModel> _orderReadRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public SyncronizationUnitFactory(
            IMongoRepository<BookReadModel> bookReadRepository,
            IMongoRepository<AuthorReadModel> authorReadRepository,
            IMongoRepository<OrderDetailReadModel> orderDetailReadRepository,
            IMongoRepository<OrderReadModel> orderReadRepository,
            IMapper mapper,
            IAuthorRepository authorRepository)
        {
            _bookReadRepository = bookReadRepository;
            _authorReadRepository = authorReadRepository;
            _orderDetailReadRepository = orderDetailReadRepository;
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }
        public ISyncronizationUnit Create(Type type)
        {
            if(type.BaseType == typeof(BookEvent))
            {
                return new BookSyncronizationUnit(_bookReadRepository, _authorReadRepository);
            }

            if(type.BaseType == typeof(AuthorEvent))
            {
                return new AuthorSyncronizationUnit(_authorReadRepository);
            }

            if (type.BaseType == typeof(DetailEvent))
            {
                return new DetailSyncronizationUnit(_orderDetailReadRepository, _bookReadRepository);
            }

            return new OrderSyncronizationUnit(_orderReadRepository, _orderDetailReadRepository, _bookReadRepository);
        }
    }
}
