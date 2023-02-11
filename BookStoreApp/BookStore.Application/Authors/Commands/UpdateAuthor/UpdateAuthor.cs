using BookStore.Application.Common.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthor : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Surname { get; init; }
        public string Name { get; init; }
    }
}
