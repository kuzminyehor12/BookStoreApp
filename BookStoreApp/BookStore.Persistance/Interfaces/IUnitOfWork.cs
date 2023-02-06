using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Interfaces
{
    public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IDetailRepository DetailRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
