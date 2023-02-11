using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IAuthorService : IService<Author, AuthorViewModel>
    {
    }
}
