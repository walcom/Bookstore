using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Book> Books
        {
            get
            {
                //throw new NotImplementedException();                
                return context.Books;
            }
        }



    }
}
