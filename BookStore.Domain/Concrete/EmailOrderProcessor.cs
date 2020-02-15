using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;


        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }


        public void Processorder(Cart cart, ShippingDetails shippingDetails)
        {
            throw new NotImplementedException();
        }


    }
}
