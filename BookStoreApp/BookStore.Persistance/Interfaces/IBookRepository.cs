using BookStore.Application.ViewModels;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetByTitleAsync(string title, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken);
    }
}
