using AutoMapper;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.DeleteBookCommand;
using BookStore.Application.Books.Commands.UpdateBook;
using BookStore.Application.Books.Queries.GetAllBooksQuery;
using BookStore.Application.Books.Queries.GetBookByIdQuery;
using BookStore.Application.Books.Queries.GetBookByIsbn;
using BookStore.Application.Books.Queries.GetBooksByAuthorId;
using BookStore.Application.Books.Queries.GetBooksByTitle;
using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using BookStore.Application.OrderDetails.Queries.GetOrderDetailsByBookId;
using BookStore.Application.OrderDetails.Queries.GetOrderDetailsByOrderId;
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
    public class BooksController : BaseController
    {
        public BooksController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [ProducesResponseType(typeof(IEnumerable<BookViewModel>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetAllBooks()
        {
            var query = new GetAllBooks();
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookViewModel), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetBookById(Guid id)
        {
            var query = new GetBookById
            {
                Id = id
            };
            var book = await _mediator.Send(query);
            return Ok(book);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookViewModel), StatusCodes.Status200OK)]
        [HttpGet("{isbn}")]
        public async Task<ActionResult<BookViewModel>> GetBookByIsbn(string isbn)
        {
            var query = new GetBookByIsbn
            {
                Isbn = isbn
            };
            var book = await _mediator.Send(query);
            return Ok(book);
        }

        [ProducesResponseType(typeof(IEnumerable<BookViewModel>), StatusCodes.Status200OK)]
        [HttpGet("{title}")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooksByTitle(string title)
        {
            var query = new GetBooksByTitle
            {
                Title = title
            };
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [ProducesResponseType(typeof(IEnumerable<BookViewModel>), StatusCodes.Status200OK)]
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooksByAuthorId(Guid authorId)
        {
            var query = new GetBooksByAuthorId
            {
                AuthorId = authorId
            };
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> CreateBook(BookWriteModel bookDto)
        {
            var command = _mapper.Map<CreateBook>(bookDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateBook), result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(Guid id, BookWriteModel bookDto)
        {
            var command = _mapper.Map<UpdateBook>(bookDto);
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var command = new DeleteBook
            {
                Id = id
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<OrderDetailViewModel>), StatusCodes.Status200OK)]
        [HttpGet("{bookId}/details")]
        public async Task<ActionResult<IEnumerable<OrderDetailViewModel>>> GetOrderDetailsByBookId(Guid bookId)
        {
            var query = new GetOrderDetailsByBookId
            {
                BookId = bookId
            };
            var details = await _mediator.Send(query);
            return Ok(details);
        }
    }
}
