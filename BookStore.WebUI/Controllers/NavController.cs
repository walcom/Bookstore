using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private IBookRepository repo;

        public NavController(IBookRepository br)
        {
            repo = br;
        }


        public PartialViewResult Menu(string specilization = null, bool mobileLayout = false)
        {
            ViewBag.SelectedSpec = specilization;
            IEnumerable<string> spec = repo.Books
                                           .Select(b => b.Specialization)
                                           .Distinct();

            string viewName = "Flexmenu"; //mobileLayout ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, spec);
            //return "Hello fom NavController";
        }


    }
}