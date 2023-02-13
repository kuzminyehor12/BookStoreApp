using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
