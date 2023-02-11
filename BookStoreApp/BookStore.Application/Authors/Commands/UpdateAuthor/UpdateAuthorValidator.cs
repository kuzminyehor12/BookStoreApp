using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthor>
    {
        public UpdateAuthorValidator()
        { 
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
