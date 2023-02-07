using AutoMapper;
using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
using BookStore.Application.Authors.Queries.GetAllAuthors;
using BookStore.Application.Authors.Queries.GetAuthorById;
using BookStore.Application.ViewModels;
using BookStore.WebApi.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        public AuthorsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }


        [ProducesResponseType(typeof(IEnumerable<AuthorViewModel>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAllAuthors()
        {
            var query = new GetAllAuthors();
            var authors = await _mediator.Send(query);
            return Ok(authors);
        }

        [ProducesResponseType(typeof(AuthorViewModel), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetAuthorById(Guid id)
        {
            var query = new GetAuthorById
            {
                Id = id
            };
            var author = await _mediator.Send(query);
            return Ok(author);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> CreateAuthor(AuthorWriteModel authorDto)
        {
            var command = _mapper.Map<CreateAuthor>(authorDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateAuthor), new { Success = result });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult> UpdateAuthor(AuthorWriteModel authorDto)
        {
            var command = _mapper.Map<UpdateAuthor>(authorDto);
            var result = await _mediator.Send(command);
            return Ok(new { Success = result });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            var command = new DeleteAuthor
            {
                Id = id
            };
            var result = await _mediator.Send(command);
            return Ok(new { Success = result });
        }
    }
}
