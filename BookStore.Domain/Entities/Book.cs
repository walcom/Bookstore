using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        
        //[HiddenInput(DisplayValue = false)]
        [Key]
        public int ISBN { get; set; }

        [Required(ErrorMessage = "Please enter a book title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a book description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a book price")]
        [Range(0.1, 99999.9, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Required]
        public string Specialization { get; set; }

        public string Author { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VersionDate { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }
}
