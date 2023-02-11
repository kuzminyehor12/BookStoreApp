using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.Validation
{
    public class BookStoreException : Exception
    {
        public BookStoreException() : base()
        {

        }

        public BookStoreException(string msg) : base(msg)
        {

        }

        public BookStoreException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}
