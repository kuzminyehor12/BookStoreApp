using FluentValidation;
using BookStore.Application.Books.Commands.CreateBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.CreateBook
{
    public class CreateBookValidator : AbstractValidator<CreateBook>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.ISBN).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AmountOnStock).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
