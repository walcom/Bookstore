using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace BookStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;


        public EmailOrderProcessor(EmailSettings setting)
        {
            emailSettings = setting;
        }


        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder().AppendLine("New order has been submitted.")
                    .AppendLine("---------------").AppendLine("Books: ");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Book.Price * line.Quantity;
                    body.AppendFormat("{0} X {1} (subtotal: {2:c}) ", line.Quantity, line.Book.Title, subtotal);
                }

                body.AppendFormat("Total order value: {0:c}", cart.GetTotalValue())
                    .AppendLine("-------").AppendLine("Ship to: ")
                    .AppendLine(shippingDetails.Name).AppendLine(shippingDetails.Line1).AppendLine(shippingDetails.Line2)
                    .AppendLine(shippingDetails.State).AppendLine(shippingDetails.City).AppendLine(shippingDetails.Country)
                .AppendLine("-------")
                .AppendFormat("Gift wrap: {0}", (shippingDetails.GiftWrap ? "Yes" : "No"));

                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress,
                    "New Order Submitted", body.ToString());

                if (emailSettings.WriteAsFile)
                    mailMessage.BodyEncoding = Encoding.ASCII;

                try
                {
                    //smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }


    }
}
