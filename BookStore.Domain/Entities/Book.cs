using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Specialization { get; set; }
        public string Author { get; set; }

    }
}
