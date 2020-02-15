using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.ModelBinding;

namespace BookStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // This method to: -  separate logic
            // - Can change the way I store cart object without change the controller
            // - other controller class can work with cart object from Customer (or any other) model binder
            // - Unit test for Cart without need of Mocking ASP.net State Management

            //throw new NotImplementedException();      modelBindingExecutionContext ModelBindingExecutionContext
            // get cart from session
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];

            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            return cart;
        }

        //bool IModelBinder.BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        //{
        //    throw new NotImplementedException();
        //}


    }
}