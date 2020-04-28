using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 3;

        public BookController(IBookRepository bookRep)
        {
            repository = bookRep;
        }


        public FileContentResult GetImage(string ISBN)
        {
            Book bo = repository.Books.FirstOrDefault(b => b.ISBN == int.Parse(ISBN));

            if (bo != null)
                return File(bo.ImageData, bo.ImageMimeType);
            else
                return null;
        }


        public ViewResult ListAll()
        {
            return View(repository.Books);
            //return List(null, 1);
        }

        public ViewResult List(string specilization, int pageno = 1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.Books
                .Where(b => b.Specialization == null || b.Specialization == specilization)
                    .OrderBy(b => b.ISBN)
                    .Skip((pageno - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageno,
                    ItemsPerPage = PageSize,
                    //TotalItems = repository.Books.Count()
                    TotalItems = (specilization == null) ?
                    repository.Books.Count() :
                    repository.Books.Where(b => b.Specialization == specilization).Count()
                },
                CurrentSpecialization = specilization
            };

            return View(model);
        }


    }
}