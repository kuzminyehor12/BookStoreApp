using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBook>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.ISBN).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AmountOnStock).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
