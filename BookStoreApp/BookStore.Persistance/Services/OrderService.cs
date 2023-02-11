using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Messaging;
using BookStore.Application.Common.Validation;
using BookStore.Application.Common.ViewModels;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;
using BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Commands.DeleteOrder;
using BookStore.Application.Orders.Commands.UpdateOrder;
using BookStore.Domain.Models;
using BookStore.Persistance.Interfaces;
using BookStore.Persistance.Validation;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Persistance.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDetailRepository _detailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public OrderService(
            IOrderRepository orderRepository,
            IDetailRepository detailRepository,
            IBookRepository bookRepository,
            IMapper mapper,
            IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _detailRepository = detailRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Result> CreateAsync(Order model, CancellationToken cancellationToken = default)
        {
            var result = await _orderRepository.CreateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new CreateOrderEvent
            {
                CreationDate = model.CreationDate,
                Discount = model.Discount,
                Total = model.Total
            });

            return result; 
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _orderRepository.DeleteAsync(id, cancellationToken);

            await _eventBus.PublishAsync(new DeleteOrderEvent
            {
                Id = id
            });

            return result;
        }

        public async Task<Result> UpdateAsync(Order model, CancellationToken cancellationToken = default)
        {
            var result = await _orderRepository.UpdateAsync(model, cancellationToken);

            await _eventBus.PublishAsync(new UpdateOrderEvent
            {
                Id = model.Id,
                CreationDate = model.CreationDate,
                Discount = model.Discount,
                Total = model.Total,
                Status = model.Status,
                ClosingDate = model.ClosingDate
            });

            return result;
        }
        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _orderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _orderRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<OrderViewModel>(entity);
        }

        public async Task<Result> AddDetailAsync(OrderDetail detail, CancellationToken cancellationToken = default)
        {
            var result = await _detailRepository.CreateAsync(detail, cancellationToken);

            await _eventBus.PublishAsync(new AddOrderDetailEvent
            {
                Amount = detail.Amount,
                OrderId = detail.OrderId,
                BookId = detail.BookId
            }, cancellationToken);
            
            return result;
        }

        public async Task<Result> ChangeDetailAmountAsync(Guid detailId, int newAmount, CancellationToken cancellationToken = default)
        {
            var detail = await _detailRepository.GetByIdAsync(detailId, cancellationToken);

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

            await _eventBus.PublishAsync(new ChangeOrderDetailAmountEvent
            {
                Id = detailId,
                Amount = newAmount
            }, cancellationToken);

            return await _detailRepository.UpdateAsync(newDetail, cancellationToken);
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            var details = await _detailRepository.GetOrderDetailsByOrderId(orderId, cancellationToken);
            var viewModels = _mapper.Map<IEnumerable<OrderDetailViewModel>>(details);

            foreach (var detail in viewModels)
            {
                detail.BookName = books.FirstOrDefault(b => details.Any(d => d.BookId == b.Id))?.Title;
                detail.BookPrice = books.FirstOrDefault(b => details.Any(d => d.BookId == b.Id))?.Price ?? default;
            }

            return viewModels;
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

            var entities = await _orderRepository.GetInDateRangeAsync((DateTime)startDate, (DateTime)endDate, cancellationToken);
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<Result> RemoveDetailAsync(Guid detailId, CancellationToken cancellationToken = default)
        {
            var result = await _detailRepository.DeleteAsync(detailId, cancellationToken);

            await _eventBus.PublishAsync(new RemoveOrderDetailEvent
            {
                Id = detailId
            }, cancellationToken);

            return result;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.GetByIdAsync(bookId, cancellationToken);

            if (book is null)
            {
                throw new BookStoreException("Entity not found");
            }

            var details = await _detailRepository.GetOrderDetailsByBookId(bookId, cancellationToken);
            var viewModels = _mapper.Map<IEnumerable<OrderDetailViewModel>>(details);

            foreach (var detail in viewModels)
            {
                detail.BookName = book.Title ?? default;
                detail.BookPrice = book.Price;
            }

            return viewModels;
        }
    }
}
