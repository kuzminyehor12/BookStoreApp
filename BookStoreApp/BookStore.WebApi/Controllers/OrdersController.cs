using AutoMapper;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;
using BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using BookStore.Application.OrderDetails.Queries.GetOrderDetailsByOrderId;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Commands.DeleteOrder;
using BookStore.Application.Orders.Commands.UpdateOrder;
using BookStore.Application.Orders.Queries.GetAllOrders;
using BookStore.Application.Orders.Queries.GetOrderById;
using BookStore.Application.Orders.Queries.GetOrdersInDateRange;
using BookStore.WebApi.Dtos;
using BookStore.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [FailureResultFilter]
    [BookStoreExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrders([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            IRequest<IEnumerable<OrderViewModel>> query;

            if(startDate is null && endDate is null)
            {
                query = new GetAllOrders();
            }
            else
            {
                query = new GetOrdersInDateRange
                {
                    StartDate = startDate,
                    EndDate = endDate
                };
            }

            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrderById(Guid id)
        {
            var query = new GetOrderById
            {
                Id = id
            };
            var order = await _mediator.Send(query);
            return Ok(order);
        }

        [ProducesResponseType(typeof(IEnumerable<OrderDetailViewModel>), StatusCodes.Status200OK)]
        [HttpGet("{orderId}/details")]
        public async Task<ActionResult<IEnumerable<OrderDetailViewModel>>> GetOrderDetailsByOrderId(Guid orderId)
        {
            var query = new GetOrderDetailsByOrderId
            {
                OrderId = orderId
            };
            var details = await _mediator.Send(query);
            return Ok(details);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderWriteModel orderDto)
        {
            var command = _mapper.Map<CreateOrder>(orderDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateOrder), result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(Guid id, OrderWriteModel orderDto)
        {
            var command = _mapper.Map<UpdateOrder>(orderDto);
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrder
            {
                Id = id
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("detail")]
        public async Task<ActionResult> AddOrderDetail(DetailWriteModel detailDto)
        {
            var command = _mapper.Map<AddOrderDetail>(detailDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(AddOrderDetail), result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("detail/{detailId}")]
        public async Task<ActionResult> RemoveOrderDetail(Guid detailId)
        {
            var command = new RemoveOrderDetail
            { 
                Id = detailId 
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("detail")]
        public async Task<ActionResult> ChangeDetailAmount(AmountWriteModel newAmountModel)
        {
            var command = new ChangeOrderDetailAmount
            {
                Id = newAmountModel.DetailId,
                Amount = newAmountModel.NewAmount
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
