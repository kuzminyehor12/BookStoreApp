using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Messaging;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.Validation;
using BookStore.Application.Common.ViewModels;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;
using BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Commands.DeleteOrder;
using BookStore.Application.Orders.Commands.UpdateOrder;
using BookStore.Domain.Models;
using BookStore.Mongo.Interfaces;
using BookStore.Mongo.Models;
using BookStore.Persistance.Interfaces;
using BookStore.Persistance.Validation;

namespace BookStore.Persistance.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public OrderService(
            IMapper mapper,
            IEventBus eventBus,
            IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result> CreateAsync(Order model, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IOrderRepository, Order>();
            var result = await repository.CreateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new CreateOrderEvent
            {
                Id = model.Id,
                CreationDate = model.CreationDate,
                Discount = model.Discount,
                Result = result.ToString()
            }, cancellationToken);

            return result; 
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IOrderRepository, Order>();
            var result = await repository.DeleteAsync(id, cancellationToken);

            await _eventBus.PublishAsync(new DeleteOrderEvent
            {
                Id = id,
                Result = result.ToString()
            }, cancellationToken);

            return result;
        }

        public async Task<Result> UpdateAsync(Order model, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IOrderRepository, Order>();
            var result = await repository.UpdateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new UpdateOrderEvent
            {
                Id = model.Id,
                CreationDate = model.CreationDate,
                Discount = model.Discount,
                Status = model.Status,
                ClosingDate = model.ClosingDate,
                Result = result.ToString()
            }, cancellationToken);

            return result;
        }
        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<OrderReadModel>();
            var entities = await repository.ToListAsync();
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<OrderReadModel>();
            var entity = await repository.FindByIdAsync(id);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<OrderViewModel>(entity);
        }

        public async Task<Result> AddDetailAsync(OrderDetail detail, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IDetailRepository, OrderDetail>();
            var result = await repository.CreateAsync(detail, cancellationToken);

            await _eventBus.PublishAsync(new AddOrderDetailEvent
            {
                Id = detail.Id,
                Amount = detail.Amount,
                OrderId = detail.OrderId,
                BookId = detail.BookId,
                Result = result.ToString()
            }, cancellationToken);
            
            return result;
        }

        public async Task<Result> ChangeDetailAmountAsync(Guid detailId, int newAmount, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IDetailRepository, OrderDetail>();

            var detail = await repository.GetByIdAsync(detailId, cancellationToken);

            if(detail is null)
            {
                throw new BookStoreException("Detail not found");
            }

            var newDetail = new OrderDetail
            {
                Id = detailId,
                Amount = newAmount,
                BookId = detail.BookId,
                OrderId = detail.OrderId
            };

            var result =  await repository.UpdateAsync(newDetail, cancellationToken);

            await _eventBus.PublishAsync(new ChangeOrderDetailAmountEvent
            {
                Id = detailId,
                Amount = newAmount,
                Result = result.ToString()
            }, cancellationToken);

            return result;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var bookRepository = await _repositoryFactory.GetCommandRepository<IBookRepository, Book>();
            var detailRepository = await _repositoryFactory.GetCommandRepository<IDetailRepository, OrderDetail>();
            var queryRepository = await _repositoryFactory.GetQueryRepository<OrderDetailReadModel>();

            var books = await bookRepository.GetAllAsync(cancellationToken);
            var details = await detailRepository.GetOrderDetailsByOrderId(orderId, cancellationToken);
            var readModels = await queryRepository.ToListAsync();
            var viewModels = readModels.Where(rm => details.Any(d => d.Id == rm.Id));

            return _mapper.Map<IEnumerable<OrderDetailViewModel>>(viewModels);
        }

        public async Task<IEnumerable<OrderViewModel>> GetInDateRangeAsync(DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
        {
            if(startDate is null)
            {
                startDate = DateTime.MinValue;
            }

            if(endDate is null)
            {
                endDate = DateTime.Now;
            }

            var repository = await _repositoryFactory.GetQueryRepository<OrderReadModel>();
            var entities = await repository.FilterBy(o => o.CreationDate >= (DateTime)startDate && o.CreationDate <= (DateTime)endDate);
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<Result> RemoveDetailAsync(Guid detailId, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetCommandRepository<IDetailRepository, OrderDetail>();
            var result = await repository.DeleteAsync(detailId, cancellationToken);

            await _eventBus.PublishAsync(new RemoveOrderDetailEvent
            {
                Id = detailId,
                Result = result.ToString()
            }, cancellationToken);

            return result;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            var repository = await _repositoryFactory.GetQueryRepository<OrderDetailReadModel>();
            var details = repository.FilterBy(d => d.Book.Id == bookId);
            return _mapper.Map<IEnumerable<OrderDetailViewModel>>(details);
        }
    }
}
