using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IBookRepository repo;

        public AdminController(IBookRepository r)
        {
            repo = r;
        }


        public ViewResult Index()
        {
            //IEnumerable<Book> books;

            //if (!string.IsNullOrEmpty(searchValue))
            //    books = from b in repo.Books
            //            where b.Description.Contains(searchValue) || b.Title.Contains(searchValue) || b.Specialization.Contains(searchValue)
            //            select b;
            //else
            //    books = from b in repo.Books select b;

            ////return View("Index", books);
            //return View(books);
            return View(repo.Books);
        }

        public ViewResult Edit(int ISBN)
        {
            Book book = repo.Books.FirstOrDefault(b => b.ISBN == ISBN);
            return View(book);
        }


        [HttpPost]
        public ActionResult Edit(Book b, HttpPostedFileWrapper upImage = null)
        {
            // opacity:30
            if (ModelState.IsValid)
            {
                if (upImage != null)
                {
                    b.ImageMimeType = upImage.ContentType;
                    b.ImageData = new byte[upImage.ContentLength];
                    upImage.InputStream.Read(b.ImageData, 0, upImage.ContentLength);
                }

                repo.SaveBook(b);
                TempData["message"] = b.Title + " has been saved.";
                return RedirectToAction("Index");
            }
            else
            {
                // working with wrong data values
                //for (int x = 0; x < ModelState.Count; x++)
                foreach (var mState in ModelState)
                    TempData["message"] += " Key: " + mState.Key + " , value: " + mState.Value + "<br/>";
                //ModelState.Keys[x].ToString(); // "Not Valid Content To Save.";

                return View(b);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Book());
        }

        [HttpGet]
        public ActionResult Delete(int ISBN)
        {
            Book deletedBook = repo.DeleteBook(ISBN);
            if (deletedBook != null)
                TempData["message"] = deletedBook.Title + " was deleted.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ViewResult Index(string searchValue)
        {
            IEnumerable<Book> books;

            if (!string.IsNullOrEmpty(searchValue))
                books = from b in repo.Books //  b.Description.Contains(searchValue) ||
                        where b.Title.IndexOf(searchValue) >= 0 || b.Specialization.IndexOf(searchValue) >= 0
                        select b;
            else
                books = from b in repo.Books select b;

            return View("Index", books);
        }


    }
}