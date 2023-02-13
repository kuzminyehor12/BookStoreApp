using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthor>
    {
        public CreateAuthorValidator()
        { 
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
