using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Book book, int qty = 1)
        {
            CartLine line = lineCollection.Where(b => b.Book.ISBN == book.ISBN).FirstOrDefault();

            if (line == null)
                lineCollection.Add(new Entities.CartLine { Book = book, Quantity = qty });
            else
                line.Quantity += qty;
        }

        public void RemoveAll(Book book)
        {
            lineCollection.RemoveAll(b => b.Book.ISBN == book.ISBN);
        }

        public decimal GetTotalValue()
        {
            return lineCollection.Sum(b => b.Book.Price * b.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }



    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }

}
