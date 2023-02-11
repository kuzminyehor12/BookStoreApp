using BookStore.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
    }
}
