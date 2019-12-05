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
    public class CartController : Controller
    {
        private IBookRepository repo;

        public CartController(IBookRepository r)
        {
            repo = r;
        }

        public RedirectToRouteResult AddToCart(int isbn, string returnUrl)
        {
            Book book = repo.Books.FirstOrDefault(b => b.ISBN == isbn);
            if(book != null)            
                GetCart().AddItem(book);

            return RedirectToAction("Index", new { returnUrl });  //returnUrl);
        }

        public RedirectToRouteResult RemoveFromCart(int isbn, string returnUrl)
        {
            Book book = repo.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
                GetCart().RemoveAll(book);

            return RedirectToAction("Index", new { returnUrl });  //returnUrl);
        }


        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        // GET: Cart
        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }


    }
}