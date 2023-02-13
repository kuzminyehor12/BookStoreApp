using BookStore.Application.Common.Models;
using BookStore.Application.Common.ViewModels;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IBookService : IService<Book, BookViewModel>
    {
        Task<BookViewModel> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookViewModel>> GetByTitleAsync(string title, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookViewModel>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default);
    }
}
