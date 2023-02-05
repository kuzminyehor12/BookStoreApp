using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthor : IRequest<bool>
    {
        public string Surname { get; set; }
        public string Name { get; set; }
    }
}
