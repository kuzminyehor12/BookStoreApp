using BookStore.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthors : IRequest<IEnumerable<AuthorViewModel>>
    {
    }
}
