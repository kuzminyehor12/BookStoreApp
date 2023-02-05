using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IBookService : IService<Book, BookViewModel>
    {
        Task<BookViewModel> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
        Task<IEnumerable<BookViewModel>> GetByTitleAsync(string isbn, CancellationToken cancellationToken);
        Task<IEnumerable<BookViewModel>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken);
    }
}
