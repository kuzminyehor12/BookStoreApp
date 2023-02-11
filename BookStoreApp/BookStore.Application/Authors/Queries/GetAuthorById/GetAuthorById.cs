using BookStore.Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorById : IRequest<AuthorViewModel>
    {
        public Guid Id { get; set; }
    }
}
