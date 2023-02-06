using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using BookStore.Persistance.Interfaces;
using BookStore.Persistance.Validation;

namespace BookStore.Persistance.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unifOfWork, IMapper mapper)
        {
            _unitOfWork = unifOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Order model, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderRepository.CreateAsync(model, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderRepository.DeleteAsync(id, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Order model, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderRepository.UpdateAsync(model, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.OrderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<OrderViewModel>(entity);
        }

        public async Task<bool> AddDetailAsync(OrderDetail detail, CancellationToken cancellationToken)
        {
            await _unitOfWork.DetailRepository.CreateAsync(detail, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ChangeDetailAmountAsync(Guid detailId, int newAmount, CancellationToken cancellationToken)
        {
            var detail = await _unitOfWork.DetailRepository.GetByIdAsync(detailId, cancellationToken);

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

            await _unitOfWork.DetailRepository.UpdateAsync(newDetail, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetDetailsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.DetailRepository.GetOrderDetailsByOrderId(orderId, cancellationToken);
            return _mapper.Map<IEnumerable<OrderDetailViewModel>>(entities);
        }

        public async Task<IEnumerable<OrderViewModel>> GetInDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.OrderRepository.GetInDateRangeAsync(startDate, endDate, cancellationToken);
            return _mapper.Map<IEnumerable<OrderViewModel>>(entities);
        }

        public async Task<bool> RemoveDetailAsync(Guid detailId, CancellationToken cancellationToken)
        {
            await _unitOfWork.DetailRepository.DeleteAsync(detailId, cancellationToken);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
