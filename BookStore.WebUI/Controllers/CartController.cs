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
        private IOrderProcessor orderProcessor;

        public CartController(IBookRepository r, IOrderProcessor proc)
        {
            repo = r;
            orderProcessor = proc;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int isbn, string returnUrl)
        {
            Book book = repo.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
                cart.AddItem(book);
            //GetCart().AddItem(book);

            return RedirectToAction("Index", new { returnUrl });  //returnUrl);
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int isbn, string returnUrl)
        {
            Book book = repo.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
                cart.RemoveAll(book);
            //GetCart().RemoveAll(book);

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
        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDeails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty");

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDeails);
                cart.Clear();
                return View("Completed");
            }
            else
                return View(shippingDeails);
        }

    }
}