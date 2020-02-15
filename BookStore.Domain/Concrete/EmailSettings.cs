using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@xbookstore.com, admin@xbookstore.com";
        public string MailFromAddress = "abc@xyz.com";

        public bool UseSsl = true;

        public string Username = "walcom2kx@gmail.com";
        public string Password = "ABCDEF123"; // fromAddress password
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\orders_bookstore_emails";
    }
}
