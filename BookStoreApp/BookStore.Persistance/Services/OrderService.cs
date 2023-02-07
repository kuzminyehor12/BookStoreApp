using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
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
        public OrderService(
            IOrderRepository orderRepository, 
            IDetailRepository detailRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _detailRepository = detailRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Order model, CancellationToken cancellationToken)
        {
            return await _orderRepository.CreateAsync(model, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Order model, CancellationToken cancellationToken)
        {
            return await _orderRepository.UpdateAsync(model, cancellationToken);
        }
        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _orderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _orderRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
            {
                throw new BookStoreException("Entity not found");
            }

            return _mapper.Map<OrderViewModel>(entity);
        }

        public async Task<bool> AddDetailAsync(OrderDetail detail, CancellationToken cancellationToken)
        {
            return await _detailRepository.CreateAsync(detail, cancellationToken);
        }

        public async Task<bool> ChangeDetailAmountAsync(Guid detailId, int newAmount, CancellationToken cancellationToken)
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

            return await _detailRepository.UpdateAsync(newDetail, cancellationToken);
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
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

        public async Task<IEnumerable<OrderViewModel>> GetInDateRangeAsync(DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
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

        public async Task<bool> RemoveDetailAsync(Guid detailId, CancellationToken cancellationToken)
        {
            return await _detailRepository.DeleteAsync(detailId, cancellationToken);
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByBookIdAsync(Guid bookId, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            var details = await _detailRepository.GetOrderDetailsByBookId(bookId, cancellationToken);
            var viewModels = _mapper.Map<IEnumerable<OrderDetailViewModel>>(details);

            foreach (var detail in viewModels)
            {
                detail.BookName = books.FirstOrDefault(b => details.Any(d => d.BookId == b.Id))?.Title;
                detail.BookPrice = books.FirstOrDefault(b => details.Any(d => d.BookId == b.Id))?.Price ?? default;
            }

            return viewModels;
        }
    }
}
