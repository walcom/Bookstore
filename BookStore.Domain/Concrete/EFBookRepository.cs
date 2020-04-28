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

        public Book DeleteBook(int ISBN)
        {
            Book dbb = context.Books.Find(ISBN);
            if (dbb != null)
            {
                context.Books.Remove(dbb);
                context.SaveChanges();
            }

            return dbb;
        }

        public void SaveBook(Book book)
        {
            Book dbb = context.Books.Find(book.ISBN);
            if (dbb == null)
                context.Books.Add(book);
            else
            {
                dbb.Title = book.Title;
                dbb.Specialization = book.Specialization;
                dbb.Price = book.Price;
                dbb.Description = book.Description;
                dbb.Author = book.Author;

                dbb.VersionDate = book.VersionDate; //DateTime.Now;
                dbb.ImageData = book.ImageData;
                dbb.ImageMimeType = book.ImageMimeType;
            }

            context.SaveChanges();
        }


    }
}
