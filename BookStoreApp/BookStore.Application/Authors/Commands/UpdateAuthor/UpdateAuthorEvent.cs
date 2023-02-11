using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorEvent : AuthorEvent
    {
        public string Surname { get; init; }
        public string Name { get; init; }
    }
}
